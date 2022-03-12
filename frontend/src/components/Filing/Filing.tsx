import React from "react"

import styles from "components/Filing/Filing.module.css"

export function Filing() {
    return (
        <div className={styles.wrapper}>
            <div className={styles.title}>备案信息</div>
            <div className={styles.item}>pythonking.top All Rights Reserved.</div>
            <div className={styles.item}>备案编号：粤 ICP 备 18141697 号</div>
            <div className={styles.item}><a href="http://www.beian.miit.gov.cn">信息产业部备案管理系统</a></div>
        </div>
    )
}
