import { actions } from "lib/actions"

export interface User {
    id: number;
    username: string;
    name: string;
    avatar: string;
    token: string;
}

export function update(data: { user: User }) {
    return {
        type: actions.USER_UPDATE,
        ...data
    }
}

export function remove() {
    return {
        type: actions.USER_REMOVE
    }
}
