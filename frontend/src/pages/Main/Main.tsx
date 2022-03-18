import React from "react"
import { Stack, Box, Grid } from "@mui/material"

import { Tweet } from "components/Tweets/Tweet"
import { CreateTweet } from "components/Tweets/CreateTweet"
import styles from "./Main.module.css"

export function Main() {
    return (
        <Grid container>
            <Grid item xs={10} className={styles.main}>
                <CreateTweet></CreateTweet>
                <Tweet tweet={{}}></Tweet>
            </Grid>
            <Grid item xs={2}>Ads</Grid>
        </Grid>
    )
}
