import { User } from "lib/user";
import { actions } from "lib/actions";

interface State {
    user: User;
    _persist: {
        version: number;
        rehydrated: boolean;
    };
}

const initialState: State = {
    user: {
        token: "",
        id: 0,
        username: "",
        name: "",
        avatar: ""
    },
    _persist: {
        version: 1,
        rehydrated: true
    }
};

const rootReducer = (state = initialState, action: any) => {
    switch (action.type) {
        case actions.USER_UPDATE:
            return {
                ...state,
                user: action.user
            };
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
            };
        default:
            return state;
    }
};

export default rootReducer;
