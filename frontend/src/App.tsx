import React from "react"
import { Provider } from "react-redux"
import { PersistGate } from "redux-persist/integration/react"
import { Outlet } from "react-router-dom"

import { configureStore } from "lib/configure-store"
import "./App.css"

function App() {
    const { store, persistor } = configureStore()
    return (
        <Provider store={store}>
            <PersistGate loading={null} persistor={persistor}>
                <p>xxx</p>
                <Outlet/>
            </PersistGate>
        </Provider>
    )
}

export default App
