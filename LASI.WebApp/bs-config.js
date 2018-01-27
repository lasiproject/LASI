﻿module.exports = {
  server: {
    baseDir: ".",
    baseUrl: ".",
    port: 3000,
    proxy: {
      target: 'localhost:3000',
      proxyRes: [function (proxyRes) {
        if (proxyRes.req.path.match(/jspm_packages|node_modules/)) {
          proxyRes.headers['cache-control'] = 'max-age=604800, public';
        }
      }]
    }
  },
  files: [
    "**/*.{html,ts,tsx,js,jsx,css}"
  ],
  watchOptions: {
    injectChanges: true,
    files: [
      "**/*.{html,js,jsx,css}"
    ],
    ignored: [
      "jspm_packages",
      "node_modules"
    ]
  },
  injectChanges: true
};
