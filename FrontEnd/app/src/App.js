import React from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Navbar, Form, FormControl, Button, Container, Row, Col } from 'react-bootstrap';
import { Editor } from './Editor.js';
import { ArticleList } from './ArticleList';
function App() {
  var Field = [
    ["grade", "年级"],
    ["unit", "单元"],
    ["index", "课文号"],
    ["subIndex", "子课文号"],
    ["title", "标题"],
    ["author", "作者"],
    ["tag","标签"],
    ["body", "正文"]
  ];
  var NoteList = { "N0": "n0", "N1": "n1" };
  let noteList = new Map()
  for (let i in NoteList)
  {
    noteList.set(i,NoteList[i])
    }
  var DemoArtleList = { "1": "课文1", "2": "课文2", "3": "课文3", "4": "课文4" };
  let demoArtleList = new Map()
  for (let i in DemoArtleList) {
    demoArtleList.set(i, DemoArtleList[i])
  }
  return (
    <div className="App">
      <header className="App-header">
        <Navbar bg='light' expand='lg' fixed="top">
          <Navbar.Brand href="#">koroneLib</Navbar.Brand>
          <Form inline>
            <FormControl type='text' placeholder='使用&符号作为分隔符' className='mr-sm-2' />
            <Button variant='outline-success'>搜索</Button>
          </Form>
        </Navbar>
      </header>
      <Container id='main'>
        <Row>
          <Col xs={4}>
            <ArticleList list={demoArtleList}/>
          </Col>
          <Col>
            <Editor fieldList={Field} noteList={noteList}/>
          </Col>
        </Row>
      </Container>
    </div>
  );
}

export default App;
