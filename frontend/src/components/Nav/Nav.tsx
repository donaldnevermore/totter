import React from "react"
import { Link, BrowserRouter } from "react-router-dom"
import { AppBar, Toolbar, Input, Box } from "@mui/material"

import { Feature } from "components/Nav/Feature"
import { LoginBar } from "components/Login/Bar"
import styles from "./Nav.module.css"

export function Nav() {
    return (
        <AppBar className={styles.whiteHeader}>
            <Toolbar className={styles.nav}>
                <Box>
                    <BrowserRouter>
                        <Link to="/">
                            <img className={styles.logo} src="/logo192.png" alt="logo" />
                            <span>Post Bar</span>
                        </Link>
                    </BrowserRouter>
                    <Input className={styles.search}
                        placeholder="搜点感兴趣的东西…"
                    />
                </Box>
                <Box >
                    <Feature />
                </Box>
                <Box >
                    <LoginBar />
                </Box>
            </Toolbar>
        </AppBar>
    )
}
