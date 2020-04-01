import { observer } from "mobx-react-lite";
import React from "react";
import { Navbar, Form, FormControl, Button } from "react-bootstrap";
import { search } from "./Controller";
import data from "./dataModel";

export const NavBar = observer(() => {
  return (
    <header className="App-header">
      <Navbar bg="light" expand="lg" fixed="top">
        <Navbar.Brand href="#">koroneLib (工程开发版)</Navbar.Brand>
        <Form inline>
          <FormControl
            type="text"
            placeholder="使用&符号作为分隔符"
            className="mr-sm-2"
            value={data.searchWord}
            onChange={(e) => {
              data.searchWord = e.target.value;
            }}
          />
          <Button
            variant="outline-success"
            onClick={(e) => {
              search(data.searchWord);
            }}
          >
            搜索
          </Button>
        </Form>
      </Navbar>
    </header>
  );
});
