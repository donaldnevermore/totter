import React, { useState } from "react"
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import { useNavigate } from "react-router-dom"
import { Tabs, Tab, IconButton, Button } from "@mui/material"

import { User, update } from "lib/user"
import { TweetList } from "components/Tweets/List"
import styles from "./Center.module.css"

interface Props {
    user: User

    update(data: { user: User }): void
}

const getBase64 = (img: any, callback: any) => {
    const reader = new FileReader()
    reader.addEventListener("load", () => callback(reader.result))
    reader.readAsDataURL(img)
}

function Center(props: Props) {
    const navigate = useNavigate()

    const { user, update } = props
    const [name, setName] = useState(user.name)
    const [avatar, setAvatar] = useState(user.avatar)
    const [file, setFile] = useState<any>(null)

    const handleNicknameChange = (event: any) => {
        setName(event.target.value.trim())
    }

    const beforeUpload = (file: any) => {
        const isJpgOrPng =
            file.type === "image/jpeg" || file.type === "image/png"
        if (!isJpgOrPng) {
            // message.error("只能上传 JPG/PNG 格式的图片");
        }
        const isLt2M = file.size / 1024 / 1024 < 2
        if (!isLt2M) {
            // message.error("图片大小不能超过 2MB");
        }
        return isJpgOrPng && isLt2M
    }

    const handleUpload = (info: any) => {
        getBase64(info.file.originFileObj, (imageUrl: string) => {
            setAvatar(imageUrl)
            setFile(info.file.originFileObj)
        })
    }

    const handleSave = async() => {
        const formData = new FormData()
        formData.append("name", name.trim())
        formData.append("avatar", file)
        try {
            // const { data } = await axios.post(
            //     `/api/users/${user.id}/`,
            //     formData,
            //     {
            //         headers: {
            //             "Content-Type": "multipart/form-data"
            //         }
            //     }
            // )
            // // message.success("保存成功");
            // update({
            //     user: {
            //         ...user,
            //         name: data.name,
            //         avatar: data.avatar
            //     }
            // })
        }
        catch (err) {
            console.log(err)
            // show ServerError();
        }
    }

    const cancel = () => {
        navigate("/")
    }

    const uploadButton = (
        <div className={styles.upload}>
            <div>上传图片</div>
        </div>
    )

    const handleTabChange = (key: any) => {
        console.log(key)
    }

    return (
        <Tabs className={styles.tabs}>
            {/* <Tab>
                <div className={styles.uploadWrapper}>
                    <Input className={styles.input} />
                    <Input className={styles.input} />

                    <div className={styles.uploadWrapper}>
                        <IconButton
                            className="avatar-uploader">
                            {avatar ? (
                                <img
                                    src={avatar}
                                    alt="avatar"
                                    style={{ width: "100%" }}
                                />
                            ) : (
                                uploadButton
                            )}
                        </IconButton>
                    </div>

                    <div className={styles.div}>
                        <Button onClick={handleSave}>
                            保存
                        </Button>
                        <Button onClick={cancel}>取消</Button>
                    </div>
                </div>
            </Tab>
            <Tab>
                <TweetList user={user} />
            </Tab>
            <Tab /> */}
        </Tabs>
    )
}

function mapStateToProps(state: any) {
    const { user } = state
    return { user }
}

function mapDispatchToProps(dispatch: any) {
    return bindActionCreators({ update }, dispatch)
}

export const PersonalCenter = connect(
    mapStateToProps,
    mapDispatchToProps
)(Center)
