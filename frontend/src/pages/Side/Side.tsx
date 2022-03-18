import React from "react"
import { Stack, Box } from "@mui/material"

import styles from "./Side.module.css"
import { TextButton } from "components/TextButton/TextButton"

export function Side() {
    return (
        <Stack className={styles.side}>
            <TextButton>Home</TextButton>
            <TextButton>Profile</TextButton>
        </Stack>
    )
}
