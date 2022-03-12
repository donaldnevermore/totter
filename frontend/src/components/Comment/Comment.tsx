import React from "react";

export function Comment(props: any) {
    const { comment } = props;

    return <div>{comment.content}</div>;
}
