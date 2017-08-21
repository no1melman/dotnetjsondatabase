import { combineReducers } from 'redux';

import { responseReducer as response } from '../response/responseRedux';

const rootReducer = combineReducers({
    response
});

export default rootReducer;