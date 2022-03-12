import React from "react";
import { Comment } from "components/Comment/Comment";

export function CommentList(props: any) {
    const { commentList } = props;
    return (
        <div>
            {commentList.map((comment: any) => (
                <Comment comment={comment} key={comment.id}/>
            ))}
        </div>
    );
}
