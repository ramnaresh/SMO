/****************************************************************************
  This file is part of the Microsoft SQL Server Code Samples.
  Copyright (C) Microsoft Corporation.  All rights reserved.

  This source code is intended only as a supplement to Microsoft
  Development Tools and/or on-line documentation.  See these other
  materials for detailed information regarding Microsoft code samples.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
*****************************************************************************/
USE master
GO

IF NOT EXISTS
  (SELECT * FROM sys.databases
   WHERE name = 'ssb_ConfigurationSample')
	
	CREATE DATABASE ssb_ConfigurationSample
GO

SET NOCOUNT ON;
GO
IF NOT EXISTS
  (SELECT * FROM sys.databases
   WHERE name = 'ssb_ConfigurationSample'
   AND is_broker_enabled = 1)
BEGIN
  ALTER DATABASE AdventureWorks SET ENABLE_BROKER ;
END ;
GO 

USE ssb_ConfigurationSample
GO
-- Option 64 terminates a query when an overflow or divide-by-zero error occurs during query execution.
sp_configure 'user options',64
GO
RECONFIGURE
GO

--Sample XML Schema's

/* ShippingRequest message schema */
CREATE XML SCHEMA COLLECTION ShippingRequestSchema AS ('<?xml version="1.0" encoding="UTF-8"?>
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema" elementFormDefault="qualified" targetNamespace="http://schemas.adventure-works.com/demo/ShippingRequest" attributeFormDefault="unqualified" xmlns:t="http://schemas.adventure-works.com/demo/ShippingRequest">
    <xs:element name="ShippingRequest" type="t:ShippingRequestType"/>
    <xs:complexType name="ShippingRequestType">
        <xs:sequence>
            <xs:element name="SalesOrderID" type="xs:string" minOccurs="1" maxOccurs="1"/>
            <xs:element name="Customer" type="t:CustomerType" minOccurs="1" maxOccurs="1"/>
            <xs:element name="ProductDetail" type="t:ProductDetailType" minOccurs="1" maxOccurs="unbounded"/>
            <xs:element name="Comment" type="xs:string" minOccurs="0" maxOccurs="1"/>
        </xs:sequence>
    </xs:complexType>
    <xs:complexType name="CustomerType">
        <xs:sequence>
            <xs:element name="fname" type="xs:string" minOccurs="1" maxOccurs="1"/>
            <xs:element name="lname" type="xs:string" minOccurs="1" maxOccurs="1"/>
            <xs:element name="addressl1" type="xs:string" minOccurs="1" maxOccurs="1"/>
            <xs:element name="addressl2" type="xs:string" minOccurs="0" maxOccurs="1"/>
            <xs:element name="city" type="xs:string" minOccurs="1" maxOccurs="1"/>
            <xs:element name="state" type="xs:string" minOccurs="1" maxOccurs="1"/>            
            <xs:element name="country" type="xs:string" minOccurs="1" maxOccurs="1"/>
            <xs:element name="zip" type="xs:string" minOccurs="0" maxOccurs="1"/>    
            <xs:element name="telephone" type="xs:string" minOccurs="0" maxOccurs="1"/>                                
        </xs:sequence>
    </xs:complexType>
    <xs:complexType name="ProductDetailType">
        <xs:sequence>
            <xs:element name="productID" type="xs:int" minOccurs="1" maxOccurs="1"/>
            <xs:element name="quantity" type="xs:int" minOccurs="1" maxOccurs="1"/>
        </xs:sequence>
    </xs:complexType>
</xs:schema>
')
GO

/* ShippingAccept message schema */
CREATE XML SCHEMA COLLECTION ShippingAcceptSchema AS ('<?xml version="1.0" encoding="UTF-8"?>
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema" elementFormDefault="qualified" targetNamespace="http://schemas.adventure-works.com/demo/ShippingAccept" attributeFormDefault="unqualified" xmlns:t="http://schemas.adventure-works.com/demo/ShippingAccept">
    <xs:element name="ShippingAccept" type="t:ShippingAcceptType"/>
    <xs:complexType name="ShippingAcceptType">
        <xs:sequence>
            <xs:element name="SalesOrderID" type="xs:string" minOccurs="1" maxOccurs="1"/>            
            <xs:element name="ShipStatus" type="t:ShipStatusType" minOccurs="0" maxOccurs="1"/>                                
        </xs:sequence>
    </xs:complexType>    
    <xs:simpleType name="ShipStatusType">
        <xs:restriction base="xs:string">
            <xs:enumeration value="Shipped"/>
            <xs:enumeration value="Unshippable"/>
        </xs:restriction>
    </xs:simpleType>    
</xs:schema>
')
GO