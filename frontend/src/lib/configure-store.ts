import { createStore } from "redux"
import { persistStore, persistReducer } from "redux-persist"
import storage from "redux-persist/lib/storage" // defaults to localStorage for web

import rootReducer from "./reducers"

const persistConfig = {
    key: "REACT_REDUX_PERSIST_DATA",
    storage
}

const persistedReducer = persistReducer(persistConfig, rootReducer)

export function configureStore() {
    const store = createStore(persistedReducer)
    const persistor = persistStore(store)
    return { store, persistor }
}
