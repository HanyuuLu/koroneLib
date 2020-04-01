import React, { Component } from "react";
import { loadEditorData } from "./Controller";
export class FileFetch extends Component {
  constructor(props) {
    super(props);
    this.fileInput = React.createRef();
  }
  render() {
    return <input type="file" id="btnFile" onChange={this.fetch.bind(this)} />;
  }
  fetch(e) {
    let files = e.target.files;
    if (files.length) {
      var file = files[0];
      var reader = new FileReader();
      if (/application\/json/.test(file.type)) {
        reader.readAsText(file);
        reader.onload = function () {
          loadEditorData(JSON.parse(this.result));
        };
      }
    }
  }
}
