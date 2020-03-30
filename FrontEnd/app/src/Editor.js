import React from 'react';
import './Editor.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Table, InputGroup, FormControl } from 'react-bootstrap';

export function Editor() {
    var Field = [
        ["grade", "年级"],
        ["unit", "单元"],
        ["index", "课文号"],
        ["subIndex", "子课文号"],
        ["title", "标题"],
        ["author", "作者"],
        ["body", "正文"]
    ];
    return <dir>

        {Field.map(i =>
            <InputGroup className="mb-3" id="inputBox">
                <InputGroup.Prepend>
                    <InputGroup.Text id="basic-addon1" id="preBox">{i[1]}</InputGroup.Text>
                </InputGroup.Prepend>
                <FormControl
                    placeholder={i[0]}
                    aria-describedby="basic-addon1"
                />
            </InputGroup>
        )}
    </dir>;
}
