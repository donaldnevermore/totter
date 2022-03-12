import React from "react"

import styles from "components/Count/Count.module.css"

export const Count = () => {
    return (
        <div className={styles.wrapper}>
            <div className={styles.title}>统计信息</div>
            <div className={styles.item}>
                <span>月活跃人数：</span>
                <span>0</span>
            </div>
            <div className={styles.item}>
                <span>在线人数：</span>
                <span>0</span>
            </div>
            <div className={styles.item}>
                <span>话题数量：</span>
                <span>0</span>
            </div>
            <div className={styles.item}>
                <span>回复数量：</span>
                <span>0</span>
            </div>
        </div>
    )
}
