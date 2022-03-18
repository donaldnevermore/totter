import React from "react"
import { Provider } from "react-redux"
import { PersistGate } from "redux-persist/integration/react"
import { Outlet } from "react-router-dom"
import { Grid } from "@mui/material"

import { configureStore } from "lib/configure-store"
import "./App.css"
import { Side } from "pages/Side/Side"

function App() {
    const { store, persistor } = configureStore()
    return (
        <Provider store={store}>
            <PersistGate loading={null} persistor={persistor}>
                <Grid container>
                    <Grid item xs={2}>
                        <Side/>
                    </Grid>
                    <Grid item xs={10}>
                        <Outlet/>
                    </Grid>
                </Grid>
            </PersistGate>
        </Provider>
    )
}

export default App
