import React from 'react';

export default class Phrase extends React.Component<models.PhraseModel, {}>{
    render() {
        return (
            this.props.hasContextmenuData
                ? <span><span> </span><span id={this.props.id}
                    className={'phrase ' + this.props.style && this.props.style.cssClass || ''}
                    title={this.props.detailText}>
                    {this.props.text}
                </span>
                </span>
                : <span><span> </span><span id={this.props.id}
                    className={'phrase ' + this.props.style && this.props.style.cssClass || ''}
                    title={this.props.detailText}>
                    {this.props.text}
                    <span> </span>
                </span>
                </span>
        );
    }
}