using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

[XmlRootAttribute("NoteData", Namespace="http://www.cpandl.com",
IsNullable = false)]
public class NoteData {

    [XmlAttribute]
    public string id;

    [XmlAttribute]
    public float x;

    [XmlAttribute]
    public float y;

    [XmlAttribute]
    public float z;

    [XmlAttribute]
    public string content;

    [XmlAttribute]
    public string action;

  public NoteData() {
    this.id = "";
    this.x = 0;
    this.y = 0;
    this.z = 0;
    this.content = "";
    this.action = "";
  }

  public NoteData(string id, float x, float y, float z, string content, string action) {
    this.id = id;
    this.x = x;
    this.y = y;
    this.z = z;
    this.content = content;
    this.action = action;
  }

}
