import React from 'react';
import { ListGroup } from 'react-bootstrap';
function SelectArticle(e, id) {
    alert(`切换到id为${id}的课文`);
}
export function ArticleList(props) {
    return <ListGroup variant='flush'>
        {/* <ListGroup.Item active onClick={(e) => { SelectArticle(e, 'A') }}>选中标题</ListGroup.Item> */}
        {[...props.list].map(([key, value]) => <ListGroup.Item onClick={(e) => { SelectArticle(e, key) }}>{value}</ListGroup.Item>)}
    </ListGroup>;
}