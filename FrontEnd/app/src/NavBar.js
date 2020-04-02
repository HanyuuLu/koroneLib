import { observer } from "mobx-react-lite";
import React from "react";
import { Navbar, FormControl, Button } from "react-bootstrap";
import { search } from "./Controller";
import data from "./dataModel";
import { IconSearch } from "./svg";
import "./NavBar.css";

export const NavBar = observer(() => {
  return (
    <header className="App-header">
      <Navbar bg="light" expand="lg" fixed="top" className="navBox">
        <Navbar.Brand href="#">koroneLib (工程开发版)</Navbar.Brand>
        <FormControl
          type="text"
          placeholder="输入搜索关键词（目前仅支持单关键词）"
          className="mr-sm-2"
          value={data.searchWord}
          onChange={(e) => {
            data.searchWord = e.target.value;
          }}
          onKeyDown={(e) => {
            if (e.keyCode === 13) {
              search(data.searchWord);
            }
          }}
        />
        <Button
          variant="outline-success"
          onClick={(e) => {
            search(data.searchWord);
          }}
        >
          <IconSearch />
        </Button>
      </Navbar>
    </header>
  );
});
