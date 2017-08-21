import 'whatwg-fetch';

const NEW_RESPONSE = 'NEW_RESPONSE';

export function responseReducer(state = { response: '' }, action) {
    if (action.type === NEW_RESPONSE) {
        return Object.assign({}, state, { response: action.response });
    }

    return state;
}

export function responseUpdateSuccessAction(response) {
    return { type: NEW_RESPONSE, response };
}

export function responseUpdateAction(uri, content) {
    const parts = uri.split(' ');
    const method = parts[0].toUpperCase();
    const rest = parts[1];

    let fetchOptions = { method };

    if (content) {
        fetchOptions = Object.assign({}, fetchOptions, { body: JSON.stringify(JSON.parse(content)) });
    }

    return function (dispatch) {
        return fetch(`/api/${rest}`, fetchOptions)
                .then(res => res.json())
                .then(response => dispatch(responseUpdateSuccessAction(response)));
    };
}