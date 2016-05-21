import React from 'react';

import Paragraph from './paragraph';

export default class DocumentViewer extends React.Component<models.DocumentModel, {}>{
    render() {
        return (
            <div className='lexical-content-block' children={this.props.paragraphs.map(({sentences}) => <Paragraph sentences={sentences}></Paragraph>) }>
            </div>
        );
    }
}
