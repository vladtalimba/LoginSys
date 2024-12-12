import User from "../interfaces/userInterface/IUser";

// Auth options.
export const authOptions = (user: User) => {
    const options = {
        headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": `${import.meta.env.VITE_CLIENT_BASE_URL}`
        },
        method: "POST",
        body: JSON.stringify(user)
    }
    return options;
}

export const signUpEndpoint = "auth/signup";
export const logInEndpoint = "auth/login";
