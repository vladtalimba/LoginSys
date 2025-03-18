import { useState } from "react";
import { NavLink } from "react-router";
import User from "../interfaces/userInterface/IUser";
import Form from "../components/Form";
import { signUpEndpoint, authOptions } from "../api/options";
import "../styles/formStyle.css";
import { useNavigate } from "react-router";
import { useAppDispatch } from "../hooks/hooks";
import { setUserState } from "../state/userState/userSlice";
import errorHandler from "../errors/errorHandler";

function SignUp() {

    const [userName, setUserName] = useState("");

    const [pass, setPass] = useState("");

    const dispatch = useAppDispatch();

    const navigate = useNavigate();

    async function submitForm(event: React.MouseEvent<HTMLButtonElement>) {
        event.preventDefault();
        const user: User = { 
            UserName: userName,
            UserPassword: pass
        }

        // TODO: error handling and redirection once we have the user.
        try {
            await postUser(user);
        } catch (err) {
            throw new Error("Something went wrong! " + err);
        }
    }

    async function postUser(user: User) {
        const options = authOptions(user);

        fetch(`${import.meta.env.VITE_API_BASE_URL}` + `${signUpEndpoint}`, options)
            .then(res => res.json())
            .then(data => {
                errorHandler(data, dispatch, setUserState, navigate, "/home");
            })
            .catch(err => {
            throw new Error(err.message);
        });
    }

    return (
        <div className="authContainer">
            <h1>Sign Up</h1>
            <Form setUserName={setUserName} setPass={setPass} submitForm={submitForm} />
          <div>
              <p>Already have an account?</p>
              <NavLink to="/">
                Log in
              </NavLink>
          </div>
      </div> 
  );
}

export default SignUp;