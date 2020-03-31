import React from "react";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import {
  Navbar,
  Form,
  FormControl,
  Button,
  Container,
  Row,
  Col,
} from "react-bootstrap";
import * as Controller from "./Controller";
import { Editor } from "./Editor";
import { ArticleList } from "./ArticleList";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <Navbar bg="light" expand="lg" fixed="top">
          <Navbar.Brand href="#">koroneLib</Navbar.Brand>
          <Form inline>
            <FormControl
              type="text"
              placeholder="使用&符号作为分隔符"
              className="mr-sm-2"
            />
            <Button variant="outline-success" onClick={Controller.search}>
              搜索
            </Button>
          </Form>
        </Navbar>
      </header>
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
