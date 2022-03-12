import React from "react"
import { useState } from "react"
import axios from "axios"
// import * as Showdown from "showdown";
// import "react-mde/lib/styles/css/react-mde-all.css";
// import ReactMde from "react-mde";
import { connect } from "react-redux"
import { InputBase, Button } from "@mui/material"

import { API } from "shared/api"
import styles from "components/Tweets/AddTweet.module.css"
import sharedStyles from "shared/styles.module.css"

declare const history: any

function Post(props: any) {
    const [title, setTitle] = useState("")
    const [content, setContent] = useState("")
    const [selectedTab, setSelectedTab] = useState<"preview" | "write">("write")

    // const converter = new Showdown.Converter({
    //     tables: true,
    //     simplifiedAutoLink: true,
    //     strikethrough: true,
    //     tasklists: true
    // });

    const handleTitleChange = (event: any) => {
        setTitle(event.target.value.trim())
    }
    const save = async () => {
        const { user } = props
        if (!user.token) {
            // Modal.warning({
            //     title: "错误",
            //     content: "请先登录"
            // });
        }
        try {
            const { data } = await axios.post(API.TWEETS, {
                title,
                content,
                user_id: user.id
            })
            if (data.err) {
                // Modal.warning({
                //     title: "错误",
                //     content: data.err
                // });
                return
            }
            history.push("/")
        }
        catch (err) {
            console.log(err)
        }
    }
    const cancel = () => {
        history.back()
    }

    return (
        <div className={styles.white}>
            <InputBase className={styles.input}
                onChange={handleTitleChange} />
            {/*<ReactMde*/}
            {/*    value={content}*/}
            {/*    onChange={setContent}*/}
            {/*    selectedTab={selectedTab}*/}
            {/*    onTabChange={setSelectedTab}*/}
            {/*    minEditorHeight={500}*/}
            {/*    maxEditorHeight={1000}*/}
            {/*    generateMarkdownPreview={(markdown: any) =>*/}
            {/*        Promise.resolve(converter.makeHtml(markdown))*/}
            {/*    }*/}
            {/*/>*/}
            <div className={styles.item}>
                <Button className={sharedStyles.button}>
                    保存
                </Button>
                <Button className={sharedStyles.button}>取消</Button>
            </div>
        </div>
    )
}

function mapStateToProps(state: any) {
    const { user } = state
    return { user }
}

export const AddTweet = connect(mapStateToProps, null)(Post)
