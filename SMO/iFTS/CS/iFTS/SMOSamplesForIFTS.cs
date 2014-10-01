/*============================================================================
  File:    SMOSamplesForIFTS.cs 

  Summary: Implements a sample code demonstrate the iFTS feature using SMO in C#.

  Date:    July 07, 2008
------------------------------------------------------------------------------
  This file is part of the Microsoft SQL Server Code Samples.

  Copyright (C) Microsoft Corporation.  All rights reserved.

  This source code is intended only as a supplement to Microsoft
  Development Tools and/or on-line documentation.  See these other
  materials for detailed information regarding Microsoft code samples.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
============================================================================*/

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Data;

namespace Microsoft.Samples.SqlServer
{
    class SMOSamplesForIFTS
    {
        static void Main()
        {   
            //Sample 1: This sample demonstrates adding and removing stopwords from a stoplist.
            AddRemoveStopWords();

            //Sample 2: This sample demonstrates creating a full-text index with an associated stoplist.
            CreateFullTextIndex();
        }

        /// <summary>
        /// This method will add and remove stopwords from a stoplist and verify the result
        /// </summary>
        public static void AddRemoveStopWords()
        {
            Console.WriteLine("Running AddRemoveStopWords Sample..."); 

            try
            {
                Server server = new Server(".");
                server.ConnectionContext.Connect();

                Console.WriteLine("Connected to '{0}' server", server.Name);

                Database db = new Database(server, "iFTSSampleDB1");
                db.Create();

                Console.WriteLine("Database '{0}' created", db.Name);

                string stopword1 = "goodbye";
                string language1 = "English";

                string stopword2 = "Auf Wiedersehen";
                string language2 = "German";

                string stopword3 = "1";
                string language3 = "Neutral";

                // Create an empty full-text stoplist
                FullTextStopList stoplist = new FullTextStopList(db, "sampleStoplist");
                stoplist.Create();

                Console.WriteLine("FullTextStoplist '{0}' created", stoplist.Name);

                // Add the above stopwords with their associated language to the full-text stoplist
                stoplist.AddStopWord(stopword1, language1);
                stoplist.AddStopWord(stopword2, language2);
                stoplist.AddStopWord(stopword3, language3);

                Console.WriteLine("Stopwords : '{0}', '{1}', '{2}' added to Stoplist '{3}'", stopword1, stopword2, stopword3, stoplist.Name);

                // Remove a stopword from the above created full-text stoplist
                stoplist.RemoveStopWord(stopword2, language2);

                Console.WriteLine("Stopword '{0}' removed from  Stoplist '{1}'", stopword2, stoplist.Name);
                Console.WriteLine();

                // Enumerate all the Stopwords in the stoplist and store them in a datatable
                DataTable dt = stoplist.EnumStopWords();
                if (dt != null)
                {
                    Console.WriteLine("Existing stopwords in the stoplist '{0}'", stoplist.Name);

                    Console.WriteLine("StopWord : Language");
                    Console.WriteLine("--------------------");
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr.IsNull("language") == false)
                        {
                            Console.WriteLine((string)dr["stopword"] + " : " + (string)dr["language"]);
                        }
                    }
                    Console.WriteLine();
                }

                // Drop the full-text stoplist
                stoplist.Drop();
                Console.WriteLine("FullTextStoplist '{0}' dropped", stoplist.Name);

                db.Drop();
                Console.WriteLine("Database '{0}' dropped", db.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {   
                Console.WriteLine("\n\n");
            }
        }

        /// <summary>
        /// This method will create a full-text index on a table with an associated stoplist
        /// </summary>
        public static void CreateFullTextIndex()
        {
            Console.WriteLine("Running CreateFullTextIndex Sample..."); 

            try
            {
                Server server = new Server(".");
                server.ConnectionContext.Connect();

                Console.WriteLine("Connected to '{0}' server", server.Name);

                Database db = new Database(server, "iFTSSampleDB2");
                db.Create();

                Console.WriteLine("Database '{0}' created", db.Name);

                // Create a default full-text catalog for the database
                FullTextCatalog ftcat = new FullTextCatalog(db, "ftcatalog");
                ftcat.IsDefault = true;
                ftcat.Create();

                Console.WriteLine("FullTextCatalog '{0}' created", ftcat.Name);

                // Add 2 columns to the table
                Table tab = new Table(db, "tab");
                tab.Columns.Add(new Column(tab, "col_fti", DataType.NVarChar(1000)));
                Column col = new Column(tab, "col_unique", DataType.Int);
                col.Nullable = false;
                tab.Columns.Add(col);

                // Create a table with a unique index on it
                Index idxUnique = new Index(tab, "tab_unique_idx");
                idxUnique.IndexKeyType = IndexKeyType.DriUniqueKey;
                idxUnique.IndexedColumns.Add(new IndexedColumn(idxUnique, col.Name));
                tab.Indexes.Add(idxUnique);
                tab.Create();

                Console.WriteLine("Table '{0}' created", tab.Name);

                // Add a column to the full-text index and associate it with a unique index
                FullTextIndex fti = new FullTextIndex(tab);
                fti.IndexedColumns.Add(new FullTextIndexColumn(fti, "col_fti"));
                fti.UniqueIndexName = idxUnique.Name;

                string stopword = "goodbye";
                string language = "English";

                // Create an empty full-text stoplist
                FullTextStopList stoplist = new FullTextStopList(db, "sampleStoplist");
                stoplist.Create();

                Console.WriteLine("FullTextStoplist '{0}' created", stoplist.Name);

                // Add a stopword to the full-text stoplist
                stoplist.AddStopWord(stopword, language);
                Console.WriteLine("Stopword '{0}' added to Stoplist '{1}'", stopword, stoplist.Name);

                fti.StopListName = "sampleStoplist";

                // Create the full-text index and associate the full-text stoplist with it
                fti.Create();

                Console.WriteLine("FullTextIndex on Table '{0}' created", tab.Name);

                fti.Drop();
                Console.WriteLine("FullTextIndex on Table '{0}' dropped", tab.Name);
                 
                ftcat.Drop();
                Console.WriteLine("FullTextCatalog '{0}' dropped", ftcat.Name);

                tab.Drop();
                Console.WriteLine("Table '{0}' created", tab.Name);

                db.Drop();
                Console.WriteLine("Database '{0}' dropped", db.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine();
            }
        }
    }
}
