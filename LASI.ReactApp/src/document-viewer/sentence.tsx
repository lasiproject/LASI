import React from 'react';

import Phrase from './phrase';

export default class Sentence extends React.Component<models.SentenceModel, {}>{
    render() {
        return (
            <div className='sentence' children={
                this.props.phrases.map(({
                    id,
                    words,
                    text,
                    style,
                    detailText,
                    contextmenu,
                    contextmenuDataSource,
                    hasContextmenuData
                }) => <Phrase
                    id={id}
                    text={text}
                    style={style}
                    words={words}
                    detailText={detailText}
                    contextmenu={contextmenu}
                    contextmenuDataSource={contextmenuDataSource}
                    hasContextmenuData={hasContextmenuData}>
                    </Phrase>)
            }>
            </div>
        );
    }
}