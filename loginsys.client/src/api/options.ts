import User from "../interfaces/userInterface/IUser";

// Auth options.
export const authOptions = (user: User) => {
    const options = {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": `${import.meta.env.VITE_CLIENT_BASE_URL}`
        },
        credentials: "include",
        body: JSON.stringify(user)
    }
    return options;
}

// Log out options.
export const signOutOptions = () => {
    const options = {
        method: "GET",
        credentials: "include",
        headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": `${import.meta.env.VITE_CLIENT_BASE_URL}`
        }
    }

    return options;
}

export const signUpEndpoint = "auth/signup";
export const logInEndpoint = "auth/login";
export const logOutEndpoint = "auth/logout";