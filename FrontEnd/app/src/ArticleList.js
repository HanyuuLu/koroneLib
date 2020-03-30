import React from 'react';
import { ListGroup } from 'react-bootstrap';
function SelectArticle(e, id) {
    alert(`切换到id为${id}的课文`);
}
export function ArticleList(props) {
    var Field = { "1": "课文1", "2": "课文2", "3": "课文3", "4": "课文4"};
    let m = new Map()
    for (let i in Field) {
        m.set(i, Field[i])
    }
    return <ListGroup variant='flush'>
        <ListGroup.Item active onClick={(e) => { SelectArticle(e, 'A') }}>选中标题</ListGroup.Item>
        {[...m].map(([key, value]) => <ListGroup.Item onClick={(e) => { SelectArticle(e, key) }}>{value}</ListGroup.Item>)}
    </ListGroup>;
}