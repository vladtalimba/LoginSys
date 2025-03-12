
function errorHandler(data, dispatch, setUserState, navigate, navUrl) {
    if (data && data.error) {
        // Custom errors from the api.
        throw new Error("" + data.message);
    } else if (data && data.errors) {
        // Built in error format.
        for (const [key, values] of Object.entries(data.errors)) {
            throw new Error("" + values[0]);
        }
    } else if (data && !data.error && !data.errors) {
        dispatch(setUserState({ Email: data.email, UserPassword: data.userPassword }));
        navigate(navUrl);
    }
}

export default errorHandler;