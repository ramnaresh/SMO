/*=====================================================================

  File:      SmoCompare.cs
  Summary:   Main SMO comparison engine.
  Date:         08-20-2004

---------------------------------------------------------------------

  This file is part of the Microsoft SQL Server Code Samples.
  Copyright (C) Microsoft Corporation.  All rights reserved.

  This source code is intended only as a supplement to Microsoft
  Development Tools and/or on-line documentation.  See these other
  materials for detailed information regarding Microsoft code samples.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
=======================================================================*/

#region Using directives

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Xml;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer;

#endregion

namespace Microsoft.Samples.SqlServer
{
    public class SmoCompare
    {
        private readonly string configFile = @"SmoCompare.xml";
        private readonly string separatorLine = new string('-', 76);
        private bool returnVal = true;
        private int level;
        private string serverName1;
        private string loginName1;
        private string password1;
        private string serverName2;
        private string loginName2;
        private string password2;
        private string currentPropertyName
            = Properties.Resources.PropertyNameNotSet;
        private Server server1;
        private Server server2;
        private SqlSmoObject smoObject1;
        private SqlSmoObject smoObject2;
        private ILogger logger;
        private PropertyComparer comparer;
        private ArrayList collectionCanBeNull;
        private StringCollection ignoreType;
        private StringCollection ignoreProperty;
        private StringCollection ignoreSchema;
        private StringCollection ignoreObject;
        private ArrayList ignorePropertyForType;
        private ArrayList ignorePropertyForObject;
        private ArrayList DiffProps;
        private StringCollection childrenOfObject1;
        private StringCollection childrenOfObject2;

        public SmoCompare()
        {
            Initialize();
        }

        public SmoCompare(ILogger logger)
        {
            this.logger = logger;
            Initialize();
        }

        private void Initialize()
        {
            comparer = new PropertyComparer();
            childrenOfObject1 = new StringCollection();
            childrenOfObject2 = new StringCollection();
            ignoreObject = new StringCollection();
            ignoreProperty = new StringCollection();
            ignoreType = new StringCollection();
            ignoreSchema = new StringCollection();
            ignorePropertyForObject = new ArrayList(3);
            ignorePropertyForType = new ArrayList(3);
            collectionCanBeNull = new ArrayList(3);
            DiffProps = new ArrayList(3);

            Configure();
        }

        // Invariant: next three following arrays must be ZERO length 
        // If the smoObject1 and smoObject2 are equal
        // Array of properties (and values) that are found different
        public ArrayList DiffProps1
        {
            get
            {
                return DiffProps;
            }
        }
        // Of Type DifferentProperties

        // This array will contain URNs pointing to objects that are in first object only
        public StringCollection ChildrenOfObject1
        {
            get
            {
                return childrenOfObject1;
            }
        }

        // This array will contain URNs pointing to objects that are in second object only
        public StringCollection ChildrenOfObject2
        {
            get
            {
                return childrenOfObject2;
            }
        }

        public string Server1
        {
            get
            {
                return this.serverName1;
            }
            set
            {
                this.serverName1 = value;
            }
        }

        public string Login1
        {
            get
            {
                return this.loginName1;
            }
            set
            {
                this.loginName1 = value;
            }
        }

        public string Password1
        {
            get
            {
                return this.password1;
            }
            set
            {
                this.password1 = value;
            }
        }

        public string Server2
        {
            get
            {
                return this.serverName2;
            }
            set
            {
                this.serverName2 = value;
            }
        }

        public string Login2
        {
            get
            {
                return this.loginName2;
            }
            set
            {
                this.loginName2 = value;
            }
        }

        public string Password2
        {
            get
            {
                return this.password2;
            }
            set
            {
                this.password2 = value;
            }
        }

        private bool ReturnValue
        {
            get
            {
                return returnVal;
            }
            set
            {
                returnVal = value;
            }
        }

        public void Clear()
        {
            DiffProps.Clear();
            childrenOfObject1.Clear();
            childrenOfObject2.Clear();
        }

        public void Reinitialize()
        {
            Configure();
        }

        /// <summary>
        /// Compare the two objects with each other.
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        private bool Compare(SqlSmoObject object1, SqlSmoObject object2)
        {
            try
            {
                level++;

                // Invariant: the objects have the same Type!!!
                if (object1.GetType().Name != object2.GetType().Name)
                {
                    LogError(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ObjectsHaveDifferentTypes,
                        object1.GetType().Name, object2.GetType().Name));
                    level--;
                    Write(Environment.NewLine);
                    throw new ApplicationException(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ObjectsHaveDifferentTypes,
                        object1.GetType().Name, object2.GetType().Name));
                }

                if (IsFromIndexCreation(object1) || IsFromIndexCreation(object2))
                {
                    level--;
                    Write(Environment.NewLine);
                    return true;
                }

                if (IsSystemNamed(object1) || IsSystemNamed(object2))
                {
                    level--;
                    Write(Environment.NewLine);
                    return true;
                }

                // Removed to facilitate comparisons of system objects.
                //if (IsSystemObject(object1) || IsSystemObject(object2))
                //{
                //    level--;
                //    Write(Environment.NewLine);
                //    return true;
                //}

                if (ShouldIgnoreSchema(object1.Urn.GetAttribute("Schema"))
                    || ShouldIgnoreSchema(object2.Urn.GetAttribute("Schema")))
                {
                    level--;
                    Write(Environment.NewLine);
                    return true;
                }

                if (ShouldIgnore(object1.GetType().Name))
                {
                    level--;
                    Write(Environment.NewLine);
                    return true;
                }

                if (IsAutoCreated(object1) || IsAutoCreated(object2))
                {
                    level--;
                    Write(Environment.NewLine);
                    return true;
                }

                // See if at least one of the object is in the ignore list; this way if the user wants to
                // Ignore an object (let's say Col1) is enough to enter Col1 of the first obj in the RED list 
                // Not both Col1 from both object (smoObject1 and smoObject2)
                if (ShouldIgnore(object1.Urn) || ShouldIgnore(object2.Urn))
                {
                    level--;
                    Write(Environment.NewLine);
                    return true;
                }

                // Iterate through all properties and ignore those from red list
                PropertyInfo[] pi1 = object1.GetType().GetProperties();
                PropertyInfo[] pi2 = object2.GetType().GetProperties();

                // Sort these two arrays based on the property names
                Array.Sort(pi1, comparer);
                Array.Sort(pi2, comparer);

                // Let's see if the number of properties are the same; 
                // If not that means we play with diferent types...
                // Which it shouldn't happen at this level...
                if (pi1.Length != pi2.Length)
                {
                    // This case is almost impossible
                    // But stuff happens hence extra tests applied :)
                    level--;
                    Write(Environment.NewLine);
                    throw new ApplicationException(string.Format(
                        System.Threading.Thread.CurrentThread.CurrentCulture,
                        Properties.Resources.DifferentNumberProperties,
                        object1.Urn, object2.Urn, pi1.Length, pi2.Length));
                }

                returnVal &= IterateProps(object1, object2, pi1, pi2);
                level--;
                Write(Environment.NewLine);

                return returnVal;
            }
            catch (ApplicationException ex)
            {
                WriteLine(string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    Properties.Resources.ExceptionWhileComparing, object1.Urn,
                    object2.Urn), MessageType.Error);
                WriteLine(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    Properties.Resources.Exception, ex));
                level--;
                Write(Environment.NewLine);

                return false;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        private bool IterateProps(SqlSmoObject object1, SqlSmoObject object2, PropertyInfo[] pi1, PropertyInfo[] pi2)
        {
            PropertyInfo propInfo1;
            PropertyInfo propInfo2;

            // Iterate through all properties
            for (int k = 0; k < pi1.Length; k++)
            {
                try
                {
                    propInfo1 = pi1[k];
                    propInfo2 = pi2[k];
                    currentPropertyName = propInfo1.Name;
                    if (ShouldIgnore(propInfo1))
                        continue;

                    if (ShouldIgnore(propInfo1.Name, object1.Urn)
                        || ShouldIgnore(propInfo2.Name, object2.Urn))
                        continue;

                    if (ShouldIgnore(propInfo1.Name, object1.GetType().Name))
                        continue;

                    if (ShouldIgnore(propInfo1.PropertyType.Name))
                        continue;

                    // RULE: We'll not compare property Name for the initial 
                    // Objects smoObject1 and smoObject2 entered by the user
                    if (propInfo1.Name == "Name" && level == 1)
                        continue;

                    // Check to see if the current prop is collection
                    if (propInfo1.PropertyType.GetInterface("ICollection",
                        true) != null)
                    {
                        AnalyzeCollection(propInfo1, propInfo2,
                            object1, object2);
                        continue;
                    }

                    // NB: This test must be AFTER the prior tests 
                    // Make sure you do not disturb this order!!!
                    if (!IsInPropBag(propInfo1, object1))
                        continue;

                    if (propInfo1.PropertyType.BaseType != null)
                    {
                        if (propInfo1.PropertyType.BaseType.FullName
                            == "System.ValueType"
                            && propInfo2.PropertyType.BaseType.FullName
                            == "System.ValueType")
                        {
                            returnVal &= CompareValueTypes(
                                propInfo1, propInfo2, object1, object2);
                            continue;
                        }
                    }
                    else
                    {
                        if (propInfo1.PropertyType.FullName == "System.ValueType"
                            && propInfo2.PropertyType.FullName == "System.ValueType")
                        {
                            returnVal &= CompareValueTypes(
                                propInfo1, propInfo2, object1, object2);
                            continue;
                        }
                    }

                    if (propInfo1.PropertyType.FullName == "System.String"
                        && propInfo2.PropertyType.FullName == "System.String")
                    {
                        returnVal &= CompareStringTypes(
                            propInfo1, propInfo2, object1, object2);
                        continue;
                    }

                    if (propInfo1.PropertyType.IsEnum && propInfo2.PropertyType.IsEnum)
                    {
                        returnVal &= CompareEnumTypes(
                            propInfo1, propInfo2, object1, object2);

                        continue;
                    }
                    else
                    {
                        // The property Type is something other than "System.Types"
                        returnVal &= CompareAnyTypes(
                            propInfo1, propInfo2, object1, object2);
                    }
                }
                catch (CollectionNotAvailableException ex)
                {
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ExceptionWhileComparing,
                        object1.Urn, object2.Urn));
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ReadingProperty,
                        currentPropertyName));
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.IgnoredException, ex),
                        MessageType.Warning);
                    WriteLine(separatorLine);

                    continue;
                }
                catch (InvalidVersionEnumeratorException ex)
                {
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ExceptionWhileComparing,
                        object1.Urn, object2.Urn));
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ReadingProperty,
                        currentPropertyName));
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.IgnoredException, ex),
                        MessageType.Warning);
                    WriteLine(separatorLine);

                    continue;
                }
                catch (UnsupportedVersionException ex)
                {
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ExceptionWhileComparing,
                        object1.Urn, object2.Urn));
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ReadingProperty,
                        currentPropertyName));
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.IgnoredException, ex),
                        MessageType.Warning);
                    WriteLine(separatorLine);

                    continue;
                }
                catch (UnknownPropertyException ex)
                {
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ExceptionWhileComparing,
                        object1.Urn, object2.Urn));
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ReadingProperty,
                        currentPropertyName));
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.IgnoredException, ex),
                        MessageType.Warning);
                    WriteLine(separatorLine);

                    continue;
                }
                catch (PropertyCannotBeRetrievedException ex)
                {
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ExceptionWhileComparing,
                        object1.Urn, object2.Urn));
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ReadingProperty,
                        currentPropertyName));
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.IgnoredException, ex),
                        MessageType.Warning);
                    WriteLine(separatorLine);

                    continue;
                }
                catch (InternalEnumeratorException ex)
                {
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ExceptionWhileComparing,
                        object1.Urn, object2.Urn));
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ReadingProperty,
                        currentPropertyName));
                    if (ex.Message.IndexOf("version") != -1
                        && ex.Message.IndexOf("is not supported") != -1)
                    {
                        WriteLine(string.Format(
                            System.Globalization.CultureInfo.InvariantCulture,
                            Properties.Resources.IgnoredException, ex),
                            MessageType.Warning);
                        WriteLine(separatorLine);

                        continue;
                    }

                    returnVal &= false;
                    WriteLine(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.Exception, ex),
                        MessageType.Error);
                    WriteLine(separatorLine);
                }
                catch (ApplicationException ex)
                {
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ExceptionWhileComparing,
                        object1.Urn, object2.Urn));
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ReadingProperty,
                        currentPropertyName));
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException is CollectionNotAvailableException)
                        {
                            WriteLine(string.Format(
                                System.Globalization.CultureInfo.InvariantCulture,
                                Properties.Resources.IgnoredException, ex),
                                MessageType.Warning);
                            WriteLine(separatorLine);

                            continue;
                        }

                        if (ex.InnerException is InvalidVersionEnumeratorException)
                        {
                            WriteLine(string.Format(
                                System.Globalization.CultureInfo.InvariantCulture,
                                Properties.Resources.IgnoredException, ex),
                                MessageType.Warning);
                            WriteLine(separatorLine);

                            continue;
                        }

                        if (ex.InnerException is UnsupportedVersionException)
                        {
                            WriteLine(string.Format(
                                System.Globalization.CultureInfo.InvariantCulture,
                                Properties.Resources.IgnoredException, ex),
                                MessageType.Warning);
                            WriteLine(separatorLine);

                            continue;
                        }

                        if (ex.InnerException is UnknownPropertyException)
                        {
                            WriteLine(string.Format(
                                System.Globalization.CultureInfo.InvariantCulture,
                                Properties.Resources.IgnoredException, ex),
                                MessageType.Warning);
                            WriteLine(separatorLine);

                            continue;
                        }

                        if (ex.InnerException is PropertyCannotBeRetrievedException)
                        {
                            WriteLine(string.Format(
                                System.Globalization.CultureInfo.InvariantCulture,
                                Properties.Resources.IgnoredException, ex),
                                MessageType.Warning);
                            WriteLine(separatorLine);

                            continue;
                        }

                        if (ex.InnerException is InternalEnumeratorException)
                        {
                            if (ex.Message.IndexOf("version") != -1
                                && ex.Message.IndexOf("is not supported") != -1)
                            {
                                WriteLine(string.Format(
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    Properties.Resources.IgnoredException, ex),
                                    MessageType.Warning);
                                WriteLine(separatorLine);

                                continue;
                            }
                        }
                    }

                    returnVal &= false;
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.Exception, ex), MessageType.Error);
                    WriteLine(separatorLine);
                }
            }

            return returnVal;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        private bool CompareAnyTypes(PropertyInfo propInfo1, PropertyInfo propInfo2, SqlSmoObject object1, SqlSmoObject object2)
        {
            bool ReturnValueLoc = true;
            object objTemp1 = propInfo1.GetValue(object1, null);
            object objTemp2 = propInfo2.GetValue(object2, null);

            if (objTemp1 == null && objTemp2 == null)
            {
                // (i.e. DefaultConstraint)
                if (!CanBeNull(propInfo1.Name, object1.GetType().Name))
                {
                    throw new ApplicationException(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.NullReferenceException,
                        object1.Urn, object2.Urn, propInfo1.Name,
                        propInfo2.Name));
                }

                // On else branch we do nothing; both props are null and they 
                // are accepted with null values
                return true;
            }

            if (objTemp1.GetType().IsSubclassOf(typeof(SqlSmoObject))
                && objTemp2.GetType().IsSubclassOf(typeof(SqlSmoObject)))
            {
                ReturnValueLoc &= Compare((SqlSmoObject)objTemp1, (SqlSmoObject)objTemp2);

                return ReturnValueLoc;
            }

            if (objTemp1.GetType().FullName
                == "Microsoft.SqlServer.Management.Smo.DataType"
                && objTemp2.GetType().FullName
                == "Microsoft.SqlServer.Management.Smo.DataType")
            {
                DataType dt1 = objTemp1 as DataType;
                DataType dt2 = objTemp2 as DataType;

                ReturnValueLoc &= dt1.SqlDataType == dt2.SqlDataType ? true : false;
                if (!ReturnValueLoc)
                {
                    DifferentProperties temp = new DifferentProperties();
                    temp.Urn1 = object1.Urn;
                    temp.Urn2 = object2.Urn;
                    temp.PropertyName = propInfo1.Name;

                    // Here we store all the prop and values for DataType
                    string objectValue1 = string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ObjectValue,
                        dt1.MaximumLength, dt1.Name, dt1.NumericPrecision,
                        dt1.NumericScale, dt1.Schema, dt1.SqlDataType);

                    string objectValue2 = string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ObjectValue,
                        dt2.MaximumLength, dt2.Name, dt2.NumericPrecision,
                        dt2.NumericScale, dt2.Schema, dt2.SqlDataType);

                    temp.ObjectValue1 = objectValue1;
                    temp.ObjectValue2 = objectValue2;

                    DiffProps.Add(temp);
                }

                return ReturnValueLoc;
            }

            ReturnValueLoc &= objTemp1.Equals(objTemp2);

            return ReturnValueLoc;
        }

        private bool CompareEnumTypes(PropertyInfo propInfo1, PropertyInfo propInfo2, SqlSmoObject object1, SqlSmoObject object2)
        {
            Enum s1 = (Enum)propInfo1.GetValue(object1, null);
            Enum s2 = (Enum)propInfo2.GetValue(object2, null);
            bool ReturnValueLoc = s1.CompareTo(s2) == 0 ? true : false;

            if (!ReturnValueLoc)
            {
                DifferentProperties temp = new DifferentProperties();
                temp.Urn1 = object1.Urn;
                temp.Urn2 = object2.Urn;
                temp.PropertyName = propInfo1.Name;
                temp.ObjectValue1
                    = propInfo1.GetValue(object1, null).ToString();
                temp.ObjectValue2
                    = propInfo2.GetValue(object2, null).ToString();
                DiffProps.Add(temp);
            }

            return ReturnValueLoc;
        }

        private bool CompareStringTypes(PropertyInfo propInfo1, PropertyInfo propInfo2, SqlSmoObject object1, SqlSmoObject object2)
        {
            bool ReturnValueLoc = false;

            if (propInfo1.Name == "TextBody")
            {
                ReturnValueLoc = (((string)propInfo1.GetValue(object1, null)))
                    .TrimEnd() == (((string)propInfo2.GetValue(object2, null)))
                    .TrimEnd() ? true : false;
            }
            else
            {
                ReturnValueLoc = ((string)propInfo1.GetValue(object1, null))
                    == ((string)propInfo2.GetValue(object2, null))
                    ? true : false;
            }

            if (!ReturnValueLoc)
            {
                DifferentProperties temp = new DifferentProperties();
                temp.Urn1 = object1.Urn;
                temp.Urn2 = object2.Urn;
                temp.PropertyName = propInfo1.Name;
                temp.ObjectValue1
                    = propInfo1.GetValue(object1, null).ToString();
                temp.ObjectValue2
                    = propInfo2.GetValue(object2, null).ToString();
                DiffProps.Add(temp);
            }

            return ReturnValueLoc;
        }

        private bool CompareValueTypes(PropertyInfo propInfo1, PropertyInfo propInfo2, SqlSmoObject object1, SqlSmoObject object2)
        {
            object obj1 = propInfo1.GetValue(object1, null);
            object obj2 = propInfo2.GetValue(object2, null);
            bool ReturnValueLoc = obj1.Equals(obj2);
            if (!ReturnValueLoc)
            {
                DifferentProperties temp = new DifferentProperties();
                temp.Urn1 = object1.Urn;
                temp.Urn2 = object2.Urn;
                temp.PropertyName = propInfo1.Name;
                temp.ObjectValue1
                    = propInfo1.GetValue(object1, null).ToString();
                temp.ObjectValue2
                    = propInfo2.GetValue(object2, null).ToString();
                DiffProps.Add(temp);
            }

            return ReturnValueLoc;
        }

        private void AnalyzeCollection(PropertyInfo propInfo1, PropertyInfo propInfo2, SqlSmoObject object1, SqlSmoObject object2)
        {
            ICollection iColl1
                = propInfo1.GetValue(object1, null) as ICollection;
            ICollection iColl2
                = propInfo2.GetValue(object2, null) as ICollection;

            IEnumerator enum1 = iColl1.GetEnumerator();
            IEnumerator enum2 = iColl2.GetEnumerator();

            // Populate the ChildrenOfObject1 and ChildrenOfObject2
            Populate1(enum1, enum2);
            Populate2(enum2, enum1);
        }

        public bool Start(Urn urn1, Urn urn2, bool compareContents)
        {
            if (!compareContents)
                return Start(urn1, urn2);
            else
                return Start(urn1, urn2) && CompareContent(urn1, urn2);
        }

        public bool Start(Urn urn1, Urn urn2)
        {
            Setup(urn1, urn2);

            if (smoObject1 == null)
            {
                throw new ApplicationException(string.Format(
                    System.Threading.Thread.CurrentThread.CurrentCulture,
                    Properties.Resources.ObjectNotCreated, urn1));
            }

            if (smoObject2 == null)
            {
                throw new ApplicationException(string.Format(
                    System.Threading.Thread.CurrentThread.CurrentCulture,
                    Properties.Resources.ObjectNotCreated, urn2));
            }

            // See if the object have the same Type; 
            if (smoObject1.GetType().Name != smoObject2.GetType().Name)
            {
                throw new ApplicationException(string.Format(
                    System.Threading.Thread.CurrentThread.CurrentCulture,
                    Properties.Resources.ObjectsHaveDifferentTypes,
                    smoObject1.GetType().Name, smoObject2.GetType().Name));
            }

            // For small databases because of the huge number of SPs it takes a lot longer than it should.
            Urn TempUrn1 = urn1;
            Urn TempUrn2 = urn2;
            while (TempUrn1.Type != "Database")
            {
                TempUrn1 = TempUrn1.Parent;
                TempUrn2 = TempUrn2.Parent;
                if (TempUrn1 == null)
                {
                    break;
                }
            }

            if (urn1.Type == "Database")
            {
                Database database1 = (Database)server1.GetSmoObject(TempUrn1);
                database1.PrefetchObjects();

                Database database2 = (Database)server2.GetSmoObject(TempUrn2);
                database2.PrefetchObjects();
            }

            return Compare(smoObject1, smoObject2);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        private void Setup(Urn urn1, Urn urn2)
        {
            if (serverName1 == null || serverName2 == null)
            {
                throw new ApplicationException(
                    Properties.Resources.ServerPropertiesCannotBeNull);
            }

            server1 = new Server(serverName1);
            server2 = new Server(serverName2);

            if (loginName1 == null || loginName1.Length == 0)
            {
                server1.ConnectionContext.LoginSecure = true;
            }
            else
            {
                server1.ConnectionContext.LoginSecure = false;
                server1.ConnectionContext.Login = loginName1;
                server1.ConnectionContext.Password = password1;
            }

            if (loginName2 == null || loginName2.Length == 0)
            {
                server2.ConnectionContext.LoginSecure = true;
            }
            else
            {
                server2.ConnectionContext.LoginSecure = false;
                server2.ConnectionContext.Login = loginName2;
                server2.ConnectionContext.Password = password2;
            }

            try
            {
                smoObject1 = server1.GetSmoObject(urn1);
            }
            catch (ApplicationException ex)
            {
                WriteLine(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    Properties.Resources.ErrorCreatingFirstObject, ex));
                throw new ApplicationException(
                    Properties.Resources.ErrorCreatingFirstObjectException,
                    ex);
            }

            try
            {
                smoObject2 = server2.GetSmoObject(urn2);
            }
            catch (ApplicationException ex)
            {
                WriteLine(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    Properties.Resources.ErrorCreatingSecondObject, ex));
                throw new ApplicationException(
                    Properties.Resources.ErrorCreatingSecondObjectException,
                    ex);
            }

            if (smoObject1 == null)
            {
                throw new ApplicationException(string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    Properties.Resources.ObjectNotCreated, urn1));
            }

            if (smoObject2 == null)
            {
                throw new ApplicationException(string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    Properties.Resources.ObjectNotCreated, urn2));
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        private void Configure()
        {
            XmlNode xmlType = null;
            XmlNode xmlSchema = null;
            XmlNode xmlProp = null;
            XmlNode xmlUrn = null;
            XmlNodeList nodeList = null;
            XmlDocument xmlDoc = new XmlDocument();
            PropertyAndType pt;
            PropertyAndObject po;

            ReturnValue = true;

            string testBin = Environment.GetEnvironmentVariable("TESTBIN");
            if (testBin == null)
                testBin = string.Empty;

            XmlTextReader xmlr = new XmlTextReader(testBin + @"\" + configFile);
            xmlr.ProhibitDtd = true;
            try
            {
                xmlDoc.Load(xmlr);
            }
            catch (Exception)
            {
                xmlr = new XmlTextReader(configFile);
                xmlr.ProhibitDtd = true;
                try
                {
                    xmlDoc.Load(xmlr);
                }
                catch (ApplicationException ex)
                {
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ErrorLoadingConfiguration, ex));
                    throw new ApplicationException(
                        Properties.Resources.ErrorLoadingConfigurationException, ex);
                }
            }

            XmlElement docElem = xmlDoc.DocumentElement;
            nodeList = docElem.GetElementsByTagName("Ignore");
            int k = 0;
            foreach (XmlNode node in nodeList)
            {
                k++;
                xmlType = node.Attributes.GetNamedItem("Type");
                xmlProp = node.Attributes.GetNamedItem("Prop");
                xmlUrn = node.Attributes.GetNamedItem("Urn");
                xmlSchema = node.Attributes.GetNamedItem("Schema");
                if (xmlType == null && xmlProp == null && xmlUrn == null && xmlSchema == null)
                {
                    throw new ApplicationException(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.BadConfigurationFile, k));
                }

                if (xmlSchema != null)
                {
                    ignoreSchema.Add(xmlSchema.Value);
                }
                else if (xmlProp == null && xmlUrn == null)
                {
                    // Ignore the Type
                    ignoreType.Add(xmlType.Value);
                }
                else if (xmlType == null && xmlProp == null)
                {
                    // Ignore the object
                    ignoreObject.Add(xmlUrn.Value);
                }
                else if (xmlType == null && xmlUrn == null)
                {
                    // Ignore the property
                    ignoreProperty.Add(xmlProp.Value);
                }
                else if (xmlUrn == null)
                {
                    // Ignore a property for a specific Type
                    pt = new PropertyAndType();
                    pt.PropertyName = xmlProp.Value;
                    pt.Type = xmlType.Value;
                    ignorePropertyForType.Add(pt);
                }
                else if (xmlType == null)
                {
                    // Ignore a property for the object
                    po = new PropertyAndObject();
                    po.PropertyName = xmlProp.Value;
                    po.Urn = new Uri(xmlUrn.Value);
                    ignorePropertyForObject.Add(po);
                }
            }

            nodeList = docElem.GetElementsByTagName("CanBeNullReference");
            k = 0;

            foreach (XmlNode node in nodeList)
            {
                k++;
                xmlType = node.Attributes.GetNamedItem("Type");
                xmlProp = node.Attributes.GetNamedItem("Prop");
                if (xmlType == null || xmlProp == null)
                {
                    throw new ApplicationException(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.BadConfigurationFileNullReference,
                        k));
                }

                pt = new PropertyAndType();
                pt.PropertyName = xmlProp.Value;
                pt.Type = xmlType.Value;
                collectionCanBeNull.Add(pt);
            }
        }

        public ILogger Logger
        {
            get
            {
                return this.logger;
            }

            set
            {
                this.logger = value;
            }
        }

        private void Write(string msg)
        {
            if (this.logger != null)
            {
                this.logger.LogMessage(msg, MessageType.Info);
            }
        }

        private void WriteLine(string msg)
        {
            if (this.logger != null)
            {
                this.logger.LogMessage(msg, MessageType.Info);
            }
        }

        private void WriteLine(string msg, MessageType msgType)
        {
            if (this.logger != null)
            {
                this.logger.LogMessage(msg, msgType);
            }
        }

        private void LogError(string msg)
        {
            if (this.logger != null)
            {
                this.logger.LogMessage(msg, MessageType.Error);
            }
        }

        private static bool IsFromIndexCreation(SqlSmoObject obj)
        {
            PropertyInfo pi = obj.GetType().GetProperty("IsFromIndexCreation",
                Type.GetType("System.Boolean"));
            if (pi != null)
                return (bool)pi.GetValue(obj, null);
            else
                return false;
        }

        private static bool IsSystemNamed(SqlSmoObject obj)
        {
            PropertyInfo pi = obj.GetType().GetProperty("IsSystemNamed",
                Type.GetType("System.Boolean"));
            if (pi != null)
                return (bool)pi.GetValue(obj, null);
            else
                return false;
        }

        private static bool IsSystemObject(SqlSmoObject obj)
        {
            PropertyInfo pi = obj.GetType().GetProperty("IsSystemObject",
                Type.GetType("System.Boolean"));
            if (pi != null)
                return (bool)pi.GetValue(obj, null);
            else
                return false;
        }

        private static bool IsAutoCreated(SqlSmoObject obj)
        {
            PropertyInfo pi = obj.GetType().GetProperty("IsAutoCreated",
                Type.GetType("System.Boolean"));
            if (pi != null)
                return (bool)pi.GetValue(obj, null);
            else
                return false;
        }

        private static bool IsInPropBag(PropertyInfo propInfo1, SqlSmoObject object1)
        {
            return object1.Properties.Contains(propInfo1.Name);
        }

        private bool ShouldIgnore(PropertyInfo prop)
        {
            return ignoreProperty.Contains(prop.Name);
        }

        private bool ShouldIgnore(string type)
        {
            return ignoreType.Contains(type);
        }

        private bool ShouldIgnore(Urn urnObject)
        {
            return ignoreObject.Contains(urnObject.ToString());
        }

        private bool ShouldIgnore(string propertyName, Urn urnObj)
        {
            bool bRet = false;

            for (int k = 0; k < ignorePropertyForObject.Count; k++)
            {
                if (((PropertyAndObject)ignorePropertyForObject[k]).PropertyName == propertyName
                    && ((PropertyAndObject)ignorePropertyForObject[k]).Urn.ToString() == urnObj.ToString())
                    return true;
            }

            return bRet;
        }

        private bool ShouldIgnore(string propertyName, string type)
        {
            bool bRet = false;

            for (int k = 0; k < ignorePropertyForType.Count; k++)
            {
                if (((PropertyAndType)ignorePropertyForType[k]).PropertyName == propertyName
                    && ((PropertyAndType)ignorePropertyForType[k]).Type == type)
                    return true;
            }

            return bRet;
        }

        private bool ShouldIgnoreSchema(string schema)
        {
            bool bRet = false;
            for (int k = 0; k < ignoreSchema.Count; k++)
            {
                if (ignoreSchema[k] == schema)
                    return true;
            }

            return bRet;
        }

        private bool CanBeNull(string propertyName, string type)
        {
            bool bRet = false;
            for (int k = 0; k < collectionCanBeNull.Count; k++)
            {
                if (((PropertyAndType)collectionCanBeNull[k]).PropertyName == propertyName
                    && ((PropertyAndType)collectionCanBeNull[k]).Type == type)
                    return true;
            }

            return bRet;
        }

        /// <summary>
        /// Populate the ChildrenOfObject1 and ChildrenOfObject2
        /// </summary>
        /// <param name="enum2"></param>
        /// <param name="enum1"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        private void Populate2(IEnumerator enum2, IEnumerator enum1)
        {
            int nFound;
            string objectName1;
            string objectName2;
            NamedSmoObject namedObject1;
            NamedSmoObject namedObject2;

            enum2.Reset();
            while (enum2.MoveNext())
            {
                enum1.Reset();
                nFound = 0;
                while (enum1.MoveNext())
                {
                    objectName1 = enum1.Current.ToString();
                    objectName2 = enum2.Current.ToString();

                    namedObject1 = enum1.Current as NamedSmoObject;
                    namedObject2 = enum2.Current as NamedSmoObject;

                    if (namedObject1 != null)
                        objectName1 = namedObject1.Name;

                    if (namedObject2 != null)
                        objectName2 = namedObject2.Name;

                    if (objectName1 == objectName2)
                    {
                        // Verify the schema name also
                        PropertyInfo piSchema1
                            = enum1.Current.GetType().GetProperty("Schema");
                        PropertyInfo piSchema2
                            = enum2.Current.GetType().GetProperty("Schema");
                        if (piSchema1 != null && piSchema2 != null)
                        {
                            if (piSchema1.GetValue(enum1.Current, null).ToString()
                                == piSchema2.GetValue(enum2.Current, null).ToString())
                            {
                                nFound = 1;
                                if (namedObject1 != null && namedObject2 != null)
                                    ReturnValue &= Compare(namedObject1, namedObject2);
                                break;
                            }
                        }
                        else
                            if (piSchema1 == null && piSchema2 == null)
                            {
                                nFound = 1;
                                if (namedObject1 != null && namedObject2 != null)
                                    ReturnValue &= Compare(namedObject1, namedObject2);
                                break;
                            }
                            else
                            {
                                // In this case one is null the other is not
                                // We throw because this case should never happen
                                throw new ApplicationException(
                                    Properties.Resources.OneTypeHasSchema);
                            }
                    }
                }

                if (nFound == 0)
                {
                    // We have to check if the object found it is a system object; if it is a system object we don't care about it (ignore it)
                    if (!IsSystemObject(((SqlSmoObject)enum2.Current))
                        && !IsAutoCreated(((SqlSmoObject)enum2.Current))
                        && !IsSystemNamed(((SqlSmoObject)enum2.Current))
                        && !IsFromIndexCreation(((SqlSmoObject)enum2.Current)))
                    {
                        childrenOfObject2.Add(((SqlSmoObject)enum2.Current).Urn.ToString());
                        ReturnValue &= false;
                    }
                }
            }
        }

        /// <summary>
        /// Populate the ChildrenOfObject1 and ChildrenOfObject2
        /// </summary>
        /// <param name="enum1"></param>
        /// <param name="enum2"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        private void Populate1(IEnumerator enum1, IEnumerator enum2)
        {
            int nFound;
            string objectName1;
            string objectName2;
            NamedSmoObject namedObject1;
            NamedSmoObject namedObject2;
            PropertyInfo piSchema1;
            PropertyInfo piSchema2;

            enum1.Reset();
            while (enum1.MoveNext())
            {
                enum2.Reset();
                nFound = 0;
                while (enum2.MoveNext())
                {
                    objectName1 = enum1.Current.ToString();
                    objectName2 = enum2.Current.ToString();

                    namedObject1 = enum1.Current as NamedSmoObject;
                    namedObject2 = enum2.Current as NamedSmoObject;

                    if (namedObject1 != null)
                        objectName1 = namedObject1.Name;

                    if (namedObject2 != null)
                        objectName2 = namedObject2.Name;

                    if (objectName1 == objectName2)
                    {
                        // Verify the schema name also
                        piSchema1 = enum1.Current.GetType().GetProperty("Schema");
                        piSchema2 = enum2.Current.GetType().GetProperty("Schema");
                        if (piSchema1 != null && piSchema2 != null)
                        {
                            if (piSchema1.GetValue(enum2.Current, null).ToString()
                                == piSchema2.GetValue(enum2.Current, null).ToString())
                            {
                                nFound = 1;
                                break;
                            }
                        }
                        else if (piSchema1 == null && piSchema2 == null)
                        {
                            nFound = 1;
                            break;
                        }
                        else
                        {
                            // In this case one is null the other is not
                            // We throw because this case should never happen
                            throw new ApplicationException(Properties.Resources.OneTypeHasSchema);
                        }
                    }
                }

                if (nFound == 0)
                {
                    // We have to check if the object found it is a system object; if it is a system object we don't care about it (ignore it)
                    if (!IsSystemObject(((SqlSmoObject)enum1.Current))
                        && !IsAutoCreated(((SqlSmoObject)enum1.Current))
                        && !IsSystemNamed(((SqlSmoObject)enum1.Current))
                        && !IsFromIndexCreation(((SqlSmoObject)enum1.Current)))
                    {
                        childrenOfObject1.Add(((SqlSmoObject)enum1.Current).Urn.ToString());
                        ReturnValue &= false;
                    }
                }
            }
        }

        public bool CompareContent(Urn urn1, Urn urn2)
        {
            bool result = true;

            // See if they are tables
            Table table1 = server1.GetSmoObject(urn1) as Table;
            Table table2 = server2.GetSmoObject(urn2) as Table;
            if (table1 != null && table2 != null)
                return CompareObjectContents(urn1, urn2, table1.Schema, table2.Schema);

            // See if they are views
            View view1 = server1.GetSmoObject(urn1) as View;
            View view2 = server2.GetSmoObject(urn2) as View;
            if (view1 != null && view2 != null)
                return CompareObjectContents(urn1, urn2, view1.Schema, view2.Schema);

            Database database1 = server1.GetSmoObject(urn1) as Database;
            Database database2 = server2.GetSmoObject(urn2) as Database;
            if (database1 == null || database2 == null)
            {
                WriteLine(Properties.Resources.ObjectsNotDatabaseEtc,
                    MessageType.Error);
                return false;
            }

            // Get all tables
            if (database1.Tables.Count != database2.Tables.Count)
            {
                WriteLine(Properties.Resources.DifferentNumberTables, MessageType.Error);
                return false;
            }

            for (int k = 0; k < database1.Tables.Count; k++)
                result &= CompareObjectContents(database1.Tables[k].Urn,
                    database2.Tables[k].Urn, database1.Tables[k].Schema,
                    database2.Tables[k].Schema);

            // Get all views
            if (database1.Views.Count != database2.Views.Count)
            {
                WriteLine(Properties.Resources.DifferentNumberViews,
                    MessageType.Error);
                return false;
            }

            for (int k = 0; k < database1.Views.Count; k++)
                result &= CompareObjectContents(database1.Views[k].Urn,
                    database2.Views[k].Urn, database1.Views[k].Schema,
                    database2.Views[k].Schema);

            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        private bool CompareObjectContents(Urn urn1, Urn urn2, string schema1, string schema2)
        {
            ArrayList sysSchemas
                = new ArrayList(new string[] { "sys", "INFORMATION_SCHEMA" });
            bool result = true;
            string objectName1 = null;
            string objectName2 = null;
            string databaseName1 = null;
            string databaseName2 = null;
            string command1 = null;
            string command2 = null;
            SqlDataReader myReader1 = null;
            SqlDataReader myReader2 = null;
            Type type1 = null;
            Type type2 = null;
            object value1 = null;
            object value2 = null;
            bool notEnd1 = false;
            bool notEnd2 = false;
            bool bAux = true;
            int nRows = 0;

            if (sysSchemas.Contains(schema1) || sysSchemas.Contains(schema2))
                return true;

            WriteLine(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                Properties.Resources.Comparing,
                urn1, urn2));

            try
            {
                try
                {
                    // This has to be a database
                    databaseName1 = urn1.Parent.GetAttribute("Name");
                    objectName1 = urn1.GetAttribute("Name");
                    command1 = string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        "SELECT * FROM [{0}].[{1}].[{2}]",
                        databaseName1, schema1, objectName1);
                    myReader1
                        = server1.ConnectionContext.ExecuteReader(command1);
                }
                catch (ApplicationException ex)
                {
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ErrorReadingFirst,
                        objectName1, ex), MessageType.Error);
                    return false;
                }

                try
                {
                    // This has to be a database
                    databaseName2 = urn2.Parent.GetAttribute("Name");
                    objectName2 = urn2.GetAttribute("Name");
                    command2 = string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        "SELECT * FROM [{0}].[{1}].[{2}]",
                        databaseName2, schema2, objectName2);
                    myReader2
                        = server2.ConnectionContext.ExecuteReader(command2);
                }
                catch (ApplicationException ex)
                {
                    WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ErrorReadingSecond,
                        objectName2, ex), MessageType.Error);

                    return false;
                }

                if (myReader1.FieldCount != myReader2.FieldCount)
                {
                    WriteLine(Properties.Resources.FieldCountDiffers,
                        MessageType.Error);
                    result = false;
                }
                else
                {
                    notEnd1 = myReader1.Read();
                    notEnd2 = myReader2.Read();
                    while (notEnd1 && notEnd2)
                    {
                        bAux = true;
                        nRows++;
                        for (int k = 0; k < myReader1.FieldCount; k++)
                        {
                            type1 = myReader1.GetFieldType(k);
                            type2 = myReader2.GetFieldType(k);
                            value1 = myReader1.GetValue(k);
                            value2 = myReader2.GetValue(k);
                            bAux = value1.Equals(value2);
                            result &= bAux;
                            if (!bAux)
                                WriteLine(string.Format(
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    Properties.Resources.Values,
                                    value1.ToString(), value2.ToString()));

                            bAux = type1.Equals(type2);
                            result &= bAux;
                            if (!bAux)
                                WriteLine(string.Format(
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    Properties.Resources.Types, type1.FullName,
                                    type2.FullName));
                        }

                        notEnd1 = myReader1.Read();
                        notEnd2 = myReader2.Read();
                    }

                    if (notEnd1 != notEnd2)
                    {
                        result = false;
                        WriteLine(string.Format(
                            System.Globalization.CultureInfo.InvariantCulture,
                            Properties.Resources.MoreRows,
                            nRows), MessageType.Error);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                WriteLine(string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    Properties.Resources.ErrorCreatingFirstObject, ex));
                throw new ApplicationException(
                    Properties.Resources.ErrorCreatingFirstObjectException,
                    ex);
            }
            finally
            {
                myReader1.Close();
                myReader2.Close();
            }

            return result;
        }
    }
}

