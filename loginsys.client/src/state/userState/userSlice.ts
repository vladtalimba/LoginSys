import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import type { RootState } from "../../store/store";
import User from "../../interfaces/userInterface/IUser";

const initialState: User = {
    UserName: "",
    UserPassword: ""
}


export const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        setUserState: (state, action: PayloadAction<User>) => {
            state.UserName = action.payload.UserName;
        }
    }

});

export const { setUserState } = userSlice.actions;
export const selectUser = (state: RootState) => state;
export default userSlice.reducer;
