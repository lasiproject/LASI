#include "stdafx.h"
#include "..\Perf\stlhelpers.h"
#include "..\Perf\NounMorpherCpp.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace stlhelpers;
namespace PerfTests {
	TEST_CLASS(NounMorpherCppTest) {
public:

	TEST_METHOD(constructor_test) {
		try {
			auto morpher = NounMorpherCpp{};
		}
		catch (const std::exception& e) {
			auto message = std::string{ e.what() };
			auto	wmessage = std::wstring{};
			std::transform(std::cbegin(message), std::cend(message), std::begin(wmessage), [](char c) { return static_cast< wchar_t >(c); });
			Assert::Fail(wmessage.c_str());
		}
	}
	};

}
