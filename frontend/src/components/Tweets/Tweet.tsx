import React from "react"
import { Link, BrowserRouter as Router } from "react-router-dom"
import { Grid, Container, Box, Avatar } from "@mui/material"

import { Info } from "components/Tweets/Info"
import { URL } from "shared/url"
import styles from "components/Tweets/Tweet.module.css"

export interface Issue {
    title: string
    id: number
    content: string
    score: number
    views: number
    created_at: number
    updated_at: number
    author: {
        id: number
        username: string
        name: string
        avatar: string
    }
}

export function Tweet(props: { issue: Issue }) {
    const { issue } = props

    return (
        <Grid container>
            <Grid item >
                <Avatar className={styles.score}>{issue.score || "?"}</Avatar>
                <Box>{issue.views}</Box>
            </Grid>
            <Grid item >
                <div className={styles.item}>
                    <Router>
                        <Link to={`${URL.TWEETS}${issue.id}`}>
                            {issue.title}
                        </Link>
                    </Router>
                </div>
                <div className={styles.content}>{issue.content}</div>
                <Info
                    author={issue.author.name}
                    createdAt={issue.created_at}
                    updatedAt={issue.updated_at}
                />
            </Grid>
        </Grid>
    )
}
