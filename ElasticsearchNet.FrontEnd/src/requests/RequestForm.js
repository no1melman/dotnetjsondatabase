import React, { Component } from 'react';

import './form.css';

class RequestForm extends Component {
    constructor() {
        super(...arguments);

        this.state = {
            doc: {
                uri: 'GET ',
                content: ''
            }
        };

        this.onChange = this.onChange.bind(this);
        this.onClick = this.onClick.bind(this);
    }

    onChange(e) {
        const { doc } = this.state;
        const { name, value } = e.target;

        const newDoc = Object.assign({}, doc, { [name]: value });
        
        this.setState({
            doc: newDoc
        });
    }

    onClick() {
        this.props.onSubmit(this.state.doc);
    }

    render () {
        const { uri, content } = this.state.doc;

        return (
            <div>
                <div className="form-input">
                    <label htmlFor="uri">URI</label>
                    <div className="form-control">
                        <input name="uri" onChange={this.onChange} value={uri} />
                    </div>
                </div>
                <div className="form-input">
                    <label htmlFor="content">Content</label>
                    <div className="form-control">
                        <textarea name="content" onChange={this.onChange} value={content} rows={10} />
                    </div>
                </div>
                <div className="form-input">
                    <div className="form-control">
                        <button className="btn-submit" onClick={this.onClick}>Submit</button>
                    </div>
                </div>
            </div>
        );
    }
}

export default RequestForm;