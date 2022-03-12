import React from "react"
import { Container } from "@mui/material"

import { Nav } from "../Nav/Nav"

export function Frame(props: any) {
    return (
        <Container>
            <Nav />
            <Container>{props.children}</Container>
        </Container>
    )
}
