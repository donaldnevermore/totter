import { createStore } from "redux"
import { persistStore, persistReducer } from "redux-persist"
import storage from "redux-persist/lib/storage" // defaults to localStorage for web

import rootReducer from "./reducers"

const persistConfig = {
    key: "react_redux_persist_data",
    storage
}

const persistedReducer = persistReducer(persistConfig, rootReducer)

export function configureStore() {
    const store = createStore(persistedReducer)
    const persistor = persistStore(store)
    return { store, persistor }
}
