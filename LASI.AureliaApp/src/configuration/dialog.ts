export default config => {
    config.useDefaults();
    config.settings.lock = false;
    config.settings.centerHorizontalOnly = false;
    config.settings.startingZIndex = 1005;
    return config;
};