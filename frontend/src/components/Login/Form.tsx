import React, { useState } from "react"
import axios from "axios"
import { useParams, useNavigate } from "react-router-dom"
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import { Grid, Box, Button, FormControl, FormLabel, RadioGroup, Radio, Input } from "@mui/material"

import { update, User } from "lib/user"
import styles from "./Form.module.css"

interface Props {
    update(data: { user: User }): void

    user: User
    form: any
}

function FormCore(props: Props) {
    const { type } = useParams()
    const navigate = useNavigate()

    const initialLoginState = Number.parseInt(type as string) === 0 ? "login" : "signup"
    const [status, setStatus] = useState<string>(initialLoginState)

    const handleTabChange = (event: any) => {
        setStatus(event.target.value)
        // form resetFields();
    }

    const onFinished = (values: any) => {
        switch (status) {
            case "login":
                login(values)
                break
            case "signup":
                signup(values)
                break
            default:
                break
        }
    }

    const checkConfirm = ({ getFieldValue }: any) => ({
        validator(rule: any, value: any) {
            if (!value || getFieldValue("password") === value) {
                return Promise.resolve()
            }
            return Promise.reject("两次输入密码不一致")
        }
    })

    const login = async (options: any) => {
        try {
            const { data } = await axios.post("/api/users/login/", options)
            if (data.error) {
                switch (data.code) {
                    case 1:
                        break
                    case 2:
                        break
                    default:
                        break
                }
                return
            }

            const { update } = props
            update({
                user: {
                    token: data.token,
                    id: data.id,
                    username: data.username,
                    name: data.name,
                    avatar: data.avatar
                }
            })
            navigate("/")
        }
        catch (e) {
            console.log(e)
            // showServerError()
        }
    }

    const signup = async (options: any) => {
        try {
            const { data } = await axios.post("/api/users/", options)
            if (data.error) {
                return
            }

            const { update } = props
            update({
                user: {
                    token: data.token,
                    id: data.id,
                    username: data.username,
                    name: data.name,
                    avatar: data.avatar
                }
            })
            navigate("/")
        }
        catch (e) {
            console.log(e)
            // showServerError()
        }
    }

    const checkUsername = (rule: any, value: any) => {
        const pattern = /^[a-zA-Z][a-zA-Z0-9_]{3,15}$/g
        if (pattern.test(value)) {
            return Promise.resolve()
        }

        return Promise.reject("用户名需要以字母开头，只能包含字母数字下划线，长度为 4 至 16 个字符")
    }


    let loginOrSignup: JSX.Element
    if (status === "login") {
        loginOrSignup = (
            <Box>
                <Button className={styles.button!} variant="contained">
                    登录
                </Button>
            </Box>
        )
    }
    else {
        loginOrSignup = (
            <>
                <Box>
                    <Input
                        type="password"
                        placeholder="请确认输入密码"
                    />
                </Box>

                <Box>
                    <Button className={styles.button!}>
                        注册
                    </Button>
                </Box>
            </>
        )
    }

    return (
        <div>
            <RadioGroup
                defaultValue={status}
                onChange={handleTabChange}>
                <Radio className={styles.radioButton!} value="login"></Radio>
                <Radio className={styles.radioButton!} value="signup"></Radio>
            </RadioGroup>

            <Box >
                <Box>
                    <Input placeholder="请输入用户名/邮箱/手机号" />
                </Box>

                <Box >
                    <Input
                        type="password"
                        placeholder="请输入密码"
                    />
                </Box>

                {loginOrSignup}
            </Box>
        </div>
    )
}

function mapStateToProps(state: any) {
    const { user } = state
    return { user }
}

function mapDispatchToProps(dispatch: any) {
    return bindActionCreators({ update }, dispatch)
}

export const LoginForm = connect(
    mapStateToProps,
    mapDispatchToProps
)(FormCore)
