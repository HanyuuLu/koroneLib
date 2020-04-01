import React from "react";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Container, Row, Col } from "react-bootstrap";
import { Editor } from "./Editor";
import { ArticleList } from "./ArticleList";
import { NavBar } from "./NavBar";

function App() {
  return (
    <div className="App">
      <NavBar />
      <Container id="main">
        <Row>
          <Col xs={4}>
            <ArticleList />
          </Col>
          <Col>
            <Editor />
          </Col>
        </Row>
      </Container>
    </div>
  );
}

export default App;
