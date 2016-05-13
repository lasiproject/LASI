module.exports = {
    "server": {
        "baseDir": "./",
        port: 3000,
        proxy: {
            target: 'localhost:3000',
            proxyRes: [function (proxyRes) {
                if (proxyRes.req.path.match(/jspm_packages/))
                    proxyRes.headers['cache-control'] = 'max-age=604800, public';
            }]
        }
    },
    "files": [
        "**/*.{html,ts,js,css}"
    ],
    "watchOptions": {
        "ignored": [
            "jspm_packages",
            "node_modules"
        ]
    },
    "injectChanges": true
};