import { useState, useEffect } from "react";
import { NavLink } from "react-router";
import User from "../interfaces/userInterface/IUser";
import { setUserState } from "../state/userState/userSlice";
import { useAppDispatch } from "../hooks/hooks";
import Form from "../components/Form";
import { authOptions, logInEndpoint } from "../api/options";
import { useNavigate } from "react-router";
import errorHandler from "../errors/errorHandler";


function LogIn() {

    const navigate = useNavigate();

    const dispatch = useAppDispatch();

    const [userName, setUserName] = useState("");

    const [pass, setPass] = useState("");

    // Validate login.
    useEffect(() => {
        verifyLogin();
    }, []);

    async function submitForm(event: React.MouseEvent<HTMLButtonElement>) {
        event.preventDefault();
        const user: User = {
            UserName: userName,
            UserPassword: pass
        }
        // TODO: redirection and error handling and validation.
        try {
            await getUser(user);
        } catch (err) {
            throw new Error("Something went wrong! " + err);
        }
    }

    async function getUser(user: User) {
        const options = authOptions(user);

        // Fetch user from db.
        fetch(`${import.meta.env.VITE_API_BASE_URL}` + `${logInEndpoint}`, options)
        .then(res => res.json())
        .then(data => {
            errorHandler(data, dispatch, setUserState, navigate, "/home");
        }).catch(err => {
            throw new Error("" + err);
        });
    }

    // TODO: find a way to handle cookie validation server-side and handle redirection here.
    async function verifyLogin() {
        
    }

    return (
        <div>
            <h1>Log In</h1>
            <Form setUserName={setUserName} setPass={setPass} submitForm={submitForm} />
            <div>
                <p>Don't have an account?</p>
                <NavLink to="signup" end>
                    Sign Up
                </NavLink>
            </div>
        </div>
    );
}

export default LogIn;