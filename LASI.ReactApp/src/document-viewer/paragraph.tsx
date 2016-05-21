import React from 'react';

import Sentence from './sentence';

export default class Paragraph extends React.Component<models.ParagraphModel, {}>{
    render() {
        const paragraph = this.props;
        return (
            <div
                className='lexical-content-block'
                children={paragraph.sentences.map(({phrases}) => <Sentence phrases={phrases}></Sentence>) }>
            </div>
        );
    }
}