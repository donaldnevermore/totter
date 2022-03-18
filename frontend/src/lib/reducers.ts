import { actions } from "lib/actions"
import { State, initialState } from "./states"

export default function rootReducer(state = initialState, action: any) {
    switch (action.type) {
    case actions.USER_UPDATE:
        return {
            ...state,
            user: action.user
        }
    case actions.USER_REMOVE:
        return {
            ...state,
            user: {
                token: "",
                id: 0,
                username: "",
                name: "",
                avatar: ""
            }
        }
    default:
        return state
    }
}
