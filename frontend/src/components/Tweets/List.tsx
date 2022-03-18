import React, { useState, useEffect } from "react"
import { Pagination } from "@mui/material"

import { Tweet } from "components/Tweets/Tweet"
import { API } from "shared/api"
import styles from "./List.module.css"

const SIZE = 50

export function TweetList(props: any) {
    const [page, setPage] = useState(1)
    const [count, setCount] = useState(0)
    const [data, setData] = useState([])

    async function getTweetList() {
        const { user } = props
        // try {
        //     const { data } = await axios.get(API.TWEETS, {
        //         params: {
        //             page,
        //             size: SIZE,
        //             user_id: user ? user.id : null
        //         }
        //     })
        //     setData(data.results)
        //     setCount(data.count)
        // }
        // catch (err) {
        //     console.log(err)
        // }
    }

    useEffect(() => {
        getTweetList()
    }, [page, SIZE])

    function handleChange(page: number) {
        setPage(page)
        getTweetList()
    }

    return (
        <div className={styles.item}>
            {/* {data.map((issue: Issue) => (
                <Tweet issue={issue} key={issue.id} />
            ))}
            <div className={styles.page}>
                <Pagination />
            </div> */}
        </div>
    )
}
