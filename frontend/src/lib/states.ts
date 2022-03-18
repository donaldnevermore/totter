import { User } from "lib/user"

export interface State {
    user: User;
    editor: {
        content: string;
    };
    _persist: {
        version: number;
        rehydrated: boolean;
    };
}

export const initialState: State = {
    user: {
        token: "",
        id: 0,
        username: "",
        name: "",
        avatar: ""
    },
    editor: {
        content: ""
    },
    _persist: {
        version: 1,
        rehydrated: true
    }
}
