import React from "react"
import { Grid } from "@mui/material"

import { Filing } from "components/Filing/Filing"
import { Count } from "components/Count/Count"

import styles from "./Side.module.css"


export function Side() {
    return (
        <Grid className={styles.side!}>
            <Count />
            <Filing />
        </Grid>
    )
}
