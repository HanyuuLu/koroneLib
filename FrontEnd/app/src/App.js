import React from "react";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Container, Row, Col } from "react-bootstrap";
import { Editor } from "./Editor";
import { ArticleList } from "./ArticleList";
import { NavBar } from "./NavBar";
import data from "./dataModel";
import { observer } from "mobx-react-lite";

const App = observer((_) => {
  if (data.showEditor === true) {
    return (
      <div className="App">
        <NavBar />
        <Container id="main">
          <Row id="row">
            <Col xs={4} className="articleList">
              <ArticleList />
            </Col>
            <Col className="editor">
              <Editor />
            </Col>
          </Row>
        </Container>
      </div>
    );
  } else {
    return (
      <div className="App">
        <NavBar />
        <Container id="main">
          <Row id="row">
            <Col className="articleList">
              <ArticleList />
            </Col>
          </Row>
        </Container>
      </div>
    );
  }
});
export default App;
