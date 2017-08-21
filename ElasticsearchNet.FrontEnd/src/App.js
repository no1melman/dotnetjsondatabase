import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import RequestForm from './requests/RequestForm';
import Spinner from './core/spinner';

import { responseUpdateAction } from './response/responseRedux';

import './app.css';

class App extends Component {
    constructor() {
        super(...arguments);

        this.state = {
            loading: false
        };

        this.onSubmit = this.onSubmit.bind(this);
    }

    onSubmit({ uri, content }) {
        this.setState({ loading: true });
        this.props.action(uri, content)
            .then(done => this.setState({ loading: false }))
            .catch(err => this.setState({ loading: false }));
    }

    render() {
        const { loading } = this.state;
        const { response } = this.props;

        let content;
        if (loading) {
            content = <Spinner />;
        } else {
            content = <pre>{JSON.stringify(response.response, null, 4)}</pre>;
        }

        return (
            <div className="container">
                <div className="nav">
                    <RequestForm 
                        onSubmit={this.onSubmit}
                    />
                </div>
                <div className="response">
                    { content }
                </div>
            </div>
        );
    }
}

const mapStateToProps = (state) => {
    return {
        response: state.response
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        action: bindActionCreators(responseUpdateAction, dispatch)
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(App);