import React, { useState } from "react"
import { Link, useNavigate } from "react-router-dom"
import { Grid, Dialog } from "@mui/material"

import styles from "./Feature.module.css"

export function Feature() {
    // const navigate = useNavigate()
    const [key, setKey] = useState("index")

    const handleClick = (item: any) => {
        if (item.disabled) {
            // Show dialog, true
            return
        }
        setKey(item.key)
        // navigate(item.url)
    }

    return (
        <Grid container>
            <Grid item className={styles.selected!} >
                <Link to="/news">新闻咨讯</Link>
            </Grid>
            <Grid item className={styles.selected!} >
                <Link to="/docs">教程文档</Link>
            </Grid>
            <Grid item className={styles.selected!} >
                <Link to="/rank">排行系统</Link>
            </Grid>
            <Grid item className={styles.selected!}>
                <Link to="/hiring">招聘信息</Link>
            </Grid>
            <Dialog open={false}></Dialog>
        </Grid>
    )
}
