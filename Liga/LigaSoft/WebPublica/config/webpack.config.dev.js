const path = require("path");
var webpack = require("webpack");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
  entry: {
    index: "./src/index.js"
  },
  output: {
    path: path.resolve(__dirname, "../dist"),
    filename: "bundle.js"
  },
  devServer: {
    contentBase: path.resolve(__dirname, "../dist"),
    compress: true,
    port: 8080,
    hot: true,
    open: true,
    inline: true,
    historyApiFallback: true,
    proxy: {
        "*": "http://localhost:58657"
      }
  },
  plugins: [new MiniCssExtractPlugin({filename: 'styles.css'}), 
            new webpack.HotModuleReplacementPlugin(), 
            new HtmlWebpackPlugin({
                inject: true,
                template: path.resolve(__dirname, "../src/index.html")})
            ],
  module: {
    rules: [
      {
        loaders: ["react-hot-loader/webpack", "babel-loader"],
        test: /\.js$/,
        exclude: /node_modules/
      },
      {
        test: /\.(svg)$/,
        use: {
          loader: 'svg-url-loader',
        },
      },
      {
        test: /\.(ttf|woff|woff2|jpg|png)$/,
        use: {
          loader: 'url-loader',
        },
      },
      {
        test: /\.css$/,
        exclude: /node_modules/,
        use: [
          {
            loader: MiniCssExtractPlugin.loader,
            options: {
              hmr: true,
            },
          },
          {
            loader: "css-loader",
            options: {
              modules: {
                localIdentName: "[local]_[hash:base64:5]",
              },
              sourceMap: true                  
            }
          }
        ]
      },
    ]
  },
  resolve: {
    alias: {
      Components: path.resolve(__dirname, '../src/components/'),
      GlobalStyle: path.resolve(__dirname, '../src/assets/styles'),
      Store: path.resolve(__dirname, '../src/store'),
      Utils: path.resolve(__dirname, '../src/utils'),
    }
  }
}