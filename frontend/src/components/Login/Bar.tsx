import React, { Component, useState } from "react"
import { Link, BrowserRouter, useNavigate } from "react-router-dom"
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import { Menu, MenuItem, Avatar, Box, IconButton } from "@mui/material"

import { remove, User } from "lib/user"
import styles from "./Bar.module.css"

function LoginBarInner(props: { user: User }) {
    // const navigate = useNavigate()
    const { user } = props
    const isLoggedIn = !!user.token

    const handleLogout = () => {
        remove()
        // navigate("/")
    }

    if (isLoggedIn) {
        return (
            <div>
                <IconButton>
                    <Avatar src={user.avatar} />
                </IconButton>
                <Menu open={true}>
                    <MenuItem>
                        <Link to={`personal/${user.id}`}>
                            <div className={styles.item}>
                                我的主页
                            </div>
                        </Link>
                    </MenuItem>
                    <MenuItem>
                        <div className={styles.item} onClick={handleLogout}>
                            退出登录
                        </div>
                    </MenuItem>
                </Menu>
            </div>
        )
    }

    return (
        <div>
            <Link to="/login">登录</Link>
            <Link to="/login?type=signup">注册</Link>
        </div>
    )
}

function mapStateToProps(state: any) {
    const { user } = state
    return { user }
}

function mapDispatchToProps(dispatch: any) {
    return bindActionCreators({ remove }, dispatch)
}

export const LoginBar = connect(
    mapStateToProps,
    mapDispatchToProps
)(LoginBarInner)
