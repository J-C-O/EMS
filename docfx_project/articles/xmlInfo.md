#  XML-Format eines Faktorbaums

Jede Experimentkonfiguration wird in einem serialisierbaren XML-Format gespeichert. Für Experimente sind die ``<Factor>`` Elemente mit ihren Attributen ``FactorName`` und ``FactorValue`` sowie ``IsActive`` relevant. Andere Elemente und Attribute dienen der EMS zum erzeugen des genauen Zustandes eines Faktorobjektes. 
Auf dieser Seite finden Sie eine Beispielkonfiguration die eingelesen werden kann, sowie die Schemadefinition der serialisierbaren XML-Dateien.
## 1 Beispiel Konfiguration eines Faktorbaums im XML Format
``

## 2 Schemadefinition

Die Schemadefinition einer lesbaren Faktorbaumkonfiguration wird durch folgendes .xsd Formate beschrieben:

`<?xml version="1.0" encoding="utf-8"?>
<xs:schema xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xs="http://www.w3.org/2001/XMLSchema" attributeFormDefault="unqualified" elementFormDefault="qualified">
	<xsd:element name="Factor">
		<xsd:complexType>
			<xsd:sequence>
				<xsd:element name="Sub-Factors">
					<xsd:complexType>
						<xsd:sequence>
							<xsd:element maxOccurs="unbounded" name="Factor">
								<xsd:complexType>
									<xsd:sequence>
										<xsd:element name="Sub-Factors">
											<xsd:complexType>
												<xsd:sequence>
													<xsd:element maxOccurs="unbounded" name="Factor">
														<xsd:complexType>
															<xsd:sequence>
																<xsd:element minOccurs="0" name="Sub-Factors">
																	<xsd:complexType>
																		<xsd:sequence>
																			<xsd:element maxOccurs="unbounded" name="Factor">
																				<xsd:complexType>
																					<xsd:sequence>
																						<xsd:element name="Values">
																							<xsd:complexType>
																								<xsd:sequence>
																									<xsd:element minOccurs="0" maxOccurs="unbounded" name="string" type="xsd:string" />
																									<xsd:element minOccurs="0" maxOccurs="unbounded" name="int" type="xsd:unsignedByte" />
																								</xsd:sequence>
																							</xsd:complexType>
																						</xsd:element>
																						<xsd:element name="ValIDX" type="xsd:unsignedByte" />
																					</xsd:sequence>
																					<xsd:attribute name="IsActive" type="xsd:boolean" use="required" />
																					<xsd:attribute name="FactorName" type="xsd:string" use="required" />
																					<xsd:attribute name="FactorValue" type="xsd:string" use="required" />
																				</xsd:complexType>
																			</xsd:element>
																		</xsd:sequence>
																	</xsd:complexType>
																</xsd:element>
																<xsd:element minOccurs="0" name="FactorIDX" type="xsd:unsignedByte" />
																<xsd:element minOccurs="0" name="Values">
																	<xsd:complexType>
																		<xsd:sequence>
																			<xsd:element maxOccurs="unbounded" name="int" type="xsd:unsignedShort" />
																		</xsd:sequence>
																	</xsd:complexType>
																</xsd:element>
																<xsd:element minOccurs="0" name="ValIDX" type="xsd:unsignedByte" />
																<xsd:element minOccurs="0" name="StartVal" type="xsd:decimal" />
																<xsd:element minOccurs="0" name="EndVal" type="xsd:decimal" />
																<xsd:element minOccurs="0" name="Increment" type="xsd:decimal" />
															</xsd:sequence>
															<xsd:attribute name="IsActive" type="xsd:boolean" use="required" />
															<xsd:attribute name="FactorName" type="xsd:string" use="required" />
															<xsd:attribute name="FactorValue" type="xsd:string" use="optional" />
														</xsd:complexType>
													</xsd:element>
												</xsd:sequence>
											</xsd:complexType>
										</xsd:element>
										<xsd:element name="FactorIDX" type="xsd:unsignedByte" />
									</xsd:sequence>
									<xsd:attribute name="IsActive" type="xsd:boolean" use="required" />
									<xsd:attribute name="FactorName" type="xsd:string" use="required" />
								</xsd:complexType>
							</xsd:element>
						</xsd:sequence>
					</xsd:complexType>
				</xsd:element>
				<xsd:element name="FactorIDX" type="xsd:unsignedByte" />
			</xsd:sequence>
			<xsd:attribute name="IsActive" type="xsd:boolean" use="required" />
			<xsd:attribute name="FactorName" type="xsd:string" use="required" />
		</xsd:complexType>
	</xsd:element>
</xs:schema>`