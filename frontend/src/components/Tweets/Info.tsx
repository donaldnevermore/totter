import React from "react"
import moment from "moment"

import styles from "./Info.module.css"

moment.locale("zh-CN")

export function Info(props: any) {
    const { author, createdAt, updatedAt } = props
    return (
        <div className={styles.info}>
            <span>{author}</span>
            <span>发布于</span>
            <span>{moment(createdAt).format("YYYY-MM-DD HH:mm")}</span>
            <span>更新于</span>
            <span>{moment(updatedAt).fromNow()}</span>
        </div>
    )
}
